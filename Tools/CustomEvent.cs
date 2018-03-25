using Eco.Gameplay.Components;
using Eco.Gameplay.Disasters;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Chat;
using Eco.Mods.TechTree;
using Eco.Shared.Utils;
using LaserControl.CustomComponent;
using System;
using System.Collections;
using System.Threading;

namespace LaserControl.Tools
{
    class CustomEvent
    {
        private static ArrayList lastPlayerActivator;
        private static OnlinePlayerComponent lastCompoActivator;

        public static void onLaserDisable()
        {

        }

        public static void onDestroyedMeteor()
        {

            if (lastPlayerActivator.Count <= 0)
            {
                Console.WriteLine("Debug pass1");
                return;
            }
                

            Console.WriteLine("LaserControl detect meteor destroyed !");

            String players = "";
            foreach (User u in lastPlayerActivator)
            {
                players += " " + u.Player.FriendlyName;
            }

            ChatManager.ServerMessageToAll(Text.Info(Text.Size(2f, $"" + players + " ont détruit l'astéroide et gagnent des récompenses")), false, Eco.Shared.Services.DefaultChatTags.Meteor, Eco.Shared.Services.ChatCategory.Info);
            ChatManager.ServerMessageToAll(Text.Info(Text.Size(1f, $"Retour à l'âge de pierre: Il semble que l'explopsion du météorite en plein vol ai détruit une grande partie de l'équipement électronique.")), false, Eco.Shared.Services.DefaultChatTags.Meteor, Eco.Shared.Services.ChatCategory.Info);


            PowerGridComponent grid = lastCompoActivator.Parent.GetComponent<PowerGridComponent>();
            Destroyer.destroyEquipement(grid);

            Rewarder.reward(lastPlayerActivator);

            LaserControl.config.updateCurrentDestruction();

            lastCompoActivator = null;

            Thread spawner = new Thread(() => SpawnMeteorThread.spawn());
            spawner.Start();
        }


        public static void onNewLaserActivation(OnlinePlayerComponent onlineCompo)
        {
            lastCompoActivator = onlineCompo;
            PowerGridNetworkComponent grid = onlineCompo.Parent.GetComponent<PowerGridNetworkComponent>();

            foreach (WorldObject o in grid.NetworkedObjects)
            {
                if (o is ComputerLabObject)
                {
                    ComputerLabObject computer = o as ComputerLabObject;
                    lastPlayerActivator = GeneralTool.getUserNear(o, 15);

                    String players = "";
                    foreach (User u in lastPlayerActivator)
                    {
                        players += " " + u.Player.FriendlyName;
                    }

                    ChatManager.ServerMessageToAll(Text.Info(Text.Size(2f, $"Activation by:" + players)), false, Eco.Shared.Services.DefaultChatTags.Meteor, Eco.Shared.Services.ChatCategory.Info);
                    return;
                }
            }
        }


    }
}
