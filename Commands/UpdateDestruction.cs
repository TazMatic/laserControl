using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Chat;
using EcoColorLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserControl.Commands
{
    class UpdateDestruction : IChatCommandHandler
    {
        [ChatCommand("updatedestruction", "Laser control increment destruction", ChatAuthorizationLevel.Admin)]
        public static void updatedestruction(User user, String argsString = "")
        {
            LaserControl.config.updateCurrentDestruction();
            user.Player.SendTemporaryMessage(LaserControl.coloredPrefix + ChatFormat.UnderLine.Value + ChatFormat.Bold.Value + ChatFormat.Green.Value + "Destruction id has been incremented");
        }






    }
}
