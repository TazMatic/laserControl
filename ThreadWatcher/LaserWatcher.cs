using Eco.Gameplay.Components;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Systems.Chat;
using Eco.Mods.TechTree;
using Eco.Shared.Utils;
using LaserControl.Tools;
using LaserControl.Config;
using System;
using System.Threading;

namespace LaserControl.ThreadWatcher
{
    class LaserWatcher
    {


        public static void LaserActivationCheck()
        {
            while(true)
            {
                checkLaserActivation();
                Thread.Sleep(1000);
            }
          
        }

        public static bool wasActive = false;

        public static void checkLaserActivation()
        {
            bool hasOneActivated = false;
            LaserObject activ = null;

            foreach (WorldObject obj in WorldObjectManager.All)
            {
                if (obj is LaserObject)
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
                Console.WriteLine("Laser control detect a new laser activation!");
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
                PowerGridComponent grid = activ.GetComponent<PowerGridComponent>();

                bool disable = false;
                if (!gridHasPower(grid))
                {
                    Console.WriteLine(LaserControlMod.config + "Stoping laser, not enought energy, this is a server patch. Demand: " + grid.EnergyDemand + " supply: " + grid.EnergySupply);
                    disable = true;
                }

                if(!gridAlwaysMatchComponent(grid))
                {
                    Console.WriteLine(LaserControlMod.config + "Stoping laser, not enought lasers, this is a server patch");
                    disable = true;
                }

                if(disable)
                {
                    disableLaser(grid);
                    ChatManager.ServerMessageToAll(Text.Info(Text.Size(1f, $"Laser has been disabled")), false, Eco.Shared.Services.DefaultChatTags.Meteor, Eco.Shared.Services.ChatCategory.Info);
                }
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

        public static void disableLaser(PowerGridComponent grid)
        {
            foreach (PowerGridComponent toDisable in grid.PowerGrid.Components)
            {
                WorldObject o = toDisable.Parent;
                if (o is LaserObject)
                {
                    LaserObject ltodisable = o as LaserObject;
                    ChargingComponent charToDisable = ltodisable.GetComponent<ChargingComponent>();
                    charToDisable.Activated = false;
                    charToDisable.TimeExpended = 0;
                    Console.WriteLine(LaserControlMod.config + "Charging component disabled...");
                }
            }
        }
        public static bool gridAlwaysMatchComponent(PowerGridComponent grid)
        {
            int nb = 0;
            bool computerFind = false;
            foreach (PowerGridComponent check in grid.PowerGrid.Components)
            {
                if(check.Parent is LaserObject)
                {
                    nb++;
                }
                else if(check.Parent is ComputerLabObject)
                {
                    computerFind = true;
                }
            }

            if (!computerFind)
                return false;

            //Restructure as to pass the config throughout the program
            ConfigGetter config = new ConfigGetter();
            if (nb < config.getLaserNeeded())
                return false;
            return true;
        }

        public static bool gridHasPower(PowerGridComponent grid)
        {
            float demand = 0;
            float suply = 0;
            foreach (PowerGridComponent check in grid.PowerGrid.Components)
            {
                demand += check.EnergyDemand;
                suply += check.EnergySupply;
            }
            if (demand > suply)
                return false;
            return true;
        }

  
    }
}
