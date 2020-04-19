using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Chat;
using Eco.Shared.Localization;
using EcoColorLib;
using System;
using System.Runtime.CompilerServices;

namespace LaserControl.Commands
{
    class UpdateDestruction : IChatCommandHandler
    {
        [ChatCommand("updatedestruction", "Laser control increment destruction", ChatAuthorizationLevel.Admin)]
        public static void updatedestruction(User user, String argsString = "")
        {
            LaserControlMod.config.updateCurrentDestruction();
            LocStringBuilder locStringBuilder = new LocStringBuilder();
            locStringBuilder.Append(LaserControlMod.coloredPrefix + ChatFormat.UnderLine.Value + ChatFormat.Bold.Value + ChatFormat.Green.Value + "Destruction id has been incremented");
            user.Player.SendTemporaryMessage(locStringBuilder.ToLocString());
        }
    }
}
