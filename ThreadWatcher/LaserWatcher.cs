using Eco.Gameplay.Components;
using Eco.Gameplay.Objects;
using Eco.Mods.TechTree;
using LaserControl.CustomComponent;
using LaserControl.Tools;
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

        private static bool needReload = false;


        public static void reloadAllObject()
        {
            Console.WriteLine(LaserControl.prefix + "Reloading all world object");
            needReload = true;
        }

        public static void LaserActivationCheck()
        {
            while(true)
            {
                checkLaserActivation();
                Thread.Sleep(1000);
            }
          
        }

        public static void OjbectModifier()
        {
            while (true)
            {

                if(needReload)
                {
                    registredObjects.Clear();
                    needReload = false;
                }

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
                        needed.RequiredItemTypes.Add(typeof(LaserObject), LaserControl.config.getLaserNeeded());

                    }
                    else if (obj is ComputerLabObject)
                    {
                        ComputerLabObject computer = obj as ComputerLabObject;
                        PowerGridNetworkComponent needed = computer.GetComponent<PowerGridNetworkComponent>();
                        needed.RequiredItemTypes.Remove(typeof(LaserObject));
                        needed.RequiredItemTypes.Add(typeof(LaserObject), LaserControl.config.getLaserNeeded());
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
                    //on remove le compo si deja present notemment utile lors du reload
                    ArrayList rl = new ArrayList();
                    foreach(OnlinePlayerComponent r in laser.GetComponents<OnlinePlayerComponent>())
                    {
                        rl.Add(r);
                    }

                    foreach(OnlinePlayerComponent r in rl)
                    {
                        laser.Components.Remove(r);
                        r.Destroy();
                    }

   

                    //online injection
                    object[] parametersArray = new object[] { typeof(OnlinePlayerComponent), new object[0] };
                    result = methodInfo.Invoke(laser, parametersArray);
                    laser.GetComponent<OnlinePlayerComponent>().Initialize();
                    Console.WriteLine(LaserControl.prefix + "Custom online player component successfuly injected in server !");




                    ArrayList torem = new ArrayList();

                    //On injecte le nouveau charging component
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



        public static bool wasActive = false;

        public static void checkLaserActivation()
        {
            bool hasOneActivated = false;
            LaserObject activ = null;

            foreach (WorldObject obj in WorldObjectManager.All)
            {
                if(obj is LaserObject)
                {
                    LaserObject laser = obj as LaserObject;

                    ChargingComponent compo = laser.GetComponent<ChargingComponent>();
                    if (compo.Activated)
                    {
                        hasOneActivated = true;
                        activ = laser;
                        break;
                    }
                    else if (!compo.Activated && wasActive)
                    {
                        wasActive = false;
                    }
                }

            }

            if(hasOneActivated&& !wasActive)
            {
                Console.WriteLine("Laser control detect a new laser activation !");
                if(activ==null)
                {
                    Console.WriteLine("Fatal error while sending destruction event !");
                    return;
                }

                wasActive = true;
                CustomEvent.onNewLaserActivation(activ);
            }
            else if(hasOneActivated && wasActive)
            {
                //continue activation update
            }
            else if(wasActive && !hasOneActivated)
            {
                //laser disabled
                CustomEvent.onLaserDisable();
                wasActive = false;
            }
            else
            {
                //continue desactivation
                wasActive = false;
            }

         

          

        }


    }
}
