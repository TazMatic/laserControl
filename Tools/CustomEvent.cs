using Eco.Gameplay.Components;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Chat;
using Eco.Mods.TechTree;
using Eco.Shared.Utils;
using System;
using System.Collections;
using System.Threading;

namespace LaserControl.Tools
{
    class CustomEvent
    {
        private static ArrayList lastPlayerActivator;
        private static LaserObject lastLaserActivator;

        public static void onLaserDisable()
        {

        }

        public static void onDestroyedMeteor()
        {

            if (lastPlayerActivator.Count <= 0)
            {
                return;
            }
                

            Console.WriteLine("LaserControl detected the meteor was destroyed!");

            String players = "";
            foreach (User u in lastPlayerActivator)
            {
                players += " " + u.Name;
            }


            ChatManager.ServerMessageToAll(Text.Info(Text.Size(2f, $"" + players + " have destroyed the meteor and earned rewards")), false, Eco.Shared.Services.DefaultChatTags.Meteor, Eco.Shared.Services.ChatCategory.Info);
            ChatManager.ServerMessageToAll(Text.Info(Text.Size(1f, $"Return to the Stone Age: It seems that the explosion of the meteorite in mid-flight destroyed a large part of the electronic equipment.")), false, Eco.Shared.Services.DefaultChatTags.Meteor, Eco.Shared.Services.ChatCategory.Info);


            PowerGridComponent grid = lastLaserActivator.GetComponent<PowerGridComponent>();
            Destroyer.destroyEquipement(grid);

            Rewarder.reward(lastPlayerActivator);

            LaserControlMod.config.updateCurrentDestruction();

            lastLaserActivator = null;

            Thread spawner = new Thread(() => SpawnMeteorThread.spawn());
            spawner.Start();
        }


        public static void onNewLaserActivation(LaserObject laser)
        {
            lastLaserActivator = laser;
            PowerGridNetworkComponent grid = lastLaserActivator.GetComponent<PowerGridNetworkComponent>();

            foreach (WorldObject o in grid.NetworkedObjects)
            {
                if (o is ComputerLabObject)
                {
                    ComputerLabObject computer = o as ComputerLabObject;
                    lastPlayerActivator = GeneralTool.getUserNear(o, 15);

                    String players = "";
                    foreach (User u in lastPlayerActivator)
                    {
                        players += " " + u.Name;
                    }

                    ChatManager.ServerMessageToAll(Text.Info(Text.Size(2f, $"Activation by:" + players)), false, Eco.Shared.Services.DefaultChatTags.Meteor, Eco.Shared.Services.ChatCategory.Info);
                    return;
                }
            }
        }


    }
}
