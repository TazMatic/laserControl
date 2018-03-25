using Eco.Gameplay.Components;
using Eco.Gameplay.Objects;
using Eco.Mods.TechTree;
using LaserControl.CustomComponent;
using System;
using System.Collections;
using System.Reflection;
using System.Threading;

namespace LaserControl.ThreadWatcher
{
    class LaserWatcher
    {
        private static Boolean stopTryUsingReflection = false;
        private static ArrayList registredObjects = new ArrayList();


        public static void OjbectModifier()
        {
            while (true)
            {
                foreach (WorldObject obj in WorldObjectManager.All)
                {
                    if (registredObjects.Contains(obj))
                    {
                        continue;
                    }
                    else if (obj is LaserObject)
                    {
                        LaserObject laser = obj as LaserObject;

                        if (!stopTryUsingReflection)
                        {
                            if (!injectNewComponent(laser))
                            {
                                stopTryUsingReflection = true;
                            }
                        }


                        PowerConsumptionComponent powerConso = laser.GetComponent<PowerConsumptionComponent>();
                        powerConso.SetPowerConsumption(LaserControl.config.getEnergyNeededForLaser());

                        PowerGridNetworkComponent needed = laser.GetComponent<PowerGridNetworkComponent>();
                        needed.RequiredItemTypes.Remove(typeof(LaserObject));
                        needed.RequiredItemTypes.Add(typeof(LaserObject), LaserControl.config.laserNeeded);

                    }
                    else if (obj is ComputerLabObject)
                    {
                        ComputerLabObject computer = obj as ComputerLabObject;
                        PowerGridNetworkComponent needed = computer.GetComponent<PowerGridNetworkComponent>();
                        needed.RequiredItemTypes.Remove(typeof(LaserObject));
                        needed.RequiredItemTypes.Add(typeof(LaserObject), LaserControl.config.laserNeeded);
                    }

                    registredObjects.Add(obj);
                }

                Thread.Sleep(5 * 1000);
            }

        }

        private static Boolean injectNewComponent(LaserObject laser)
        {
            MethodInfo methodInfo = null;

            foreach (MethodInfo met in typeof(WorldObject).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
            {

                if (met.Name.Contains("AddComponent"))
                {
                    ParameterInfo[] parameters = met.GetParameters();

                    if (parameters.Length == 2)
                    {
                        methodInfo = met;
                        break;
                    }
                }

            }

            if (methodInfo != null)
            {
                object result = null;
                ParameterInfo[] parameters = methodInfo.GetParameters();


                if (parameters.Length == 0)
                {
                    Console.WriteLine(LaserControl.prefix + "Invoking lethod with no args !!! This is a problem, please contact developer");
                    result = methodInfo.Invoke(laser, null);
                }
                else
                {

                    //online injection
                    object[] parametersArray = new object[] { typeof(OnlinePlayerComponent), new object[0] };
                    result = methodInfo.Invoke(laser, parametersArray);
                    laser.GetComponent<OnlinePlayerComponent>().Initialize();
                    Console.WriteLine(LaserControl.prefix + "Custom online player component successfuly injected in server !");




                    ArrayList torem = new ArrayList();

                    //charging injection
                    foreach (ChargingComponent compo in laser.GetComponents<ChargingComponent>())
                    {
                        torem.Add(compo);
                    }

                    foreach (ChargingComponent compo in torem)
                    {
                        laser.Components.Remove(compo);
                        compo.Destroy();
                    }
                        

                    if (laser.HasComponent<ChargingComponent>())
                    {
                        Console.WriteLine(LaserControl.prefix + "Failed to remove basic charging component");
                    }
                    else
                    {
                        parametersArray = new object[] { typeof(ChargingComponent), new object[0] };
                        result = methodInfo.Invoke(laser, parametersArray);
                        laser.GetComponent<ChargingComponent>().Initialize(LaserControl.config.secondsToDestroyMeteor, LaserControl.config.secondsToDestroyMeteor);
                        Console.WriteLine(LaserControl.prefix + "New charging component injected !");
                    }


                    return true;
                }

            }
            else
            {
                Console.WriteLine(LaserControl.prefix + "Can't create new component, error while finding reflection method AddComponent in WorldObject class");
            }

            return false;

        }


    }
}
