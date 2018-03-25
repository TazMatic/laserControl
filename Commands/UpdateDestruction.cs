﻿using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Chat;
using EcoColorLib;
using System;

namespace LaserControl.Commands
{
    class UpdateDestruction : IChatCommandHandler
    {
        [ChatCommand("updatedestruction", "Laser control increment destruction", ChatAuthorizationLevel.Admin)]
        public static void updatedestruction(User user, String argsString = "")
        {
            LaserControlMod.config.updateCurrentDestruction();
            user.Player.SendTemporaryMessage(LaserControlMod.coloredPrefix + ChatFormat.UnderLine.Value + ChatFormat.Bold.Value + ChatFormat.Green.Value + "Destruction id has been incremented");
        }






    }
}
