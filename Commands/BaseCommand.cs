using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Chat;
using EcoColorLib;
using System;

namespace LaserControl.Commands
{
    class BaseCommand : IChatCommandHandler
    {
        [ChatCommand("laserconstrol", "Laser control base command L", ChatAuthorizationLevel.User)]
        public static void LaserCommandL(User user, String argsString = "")
        {
            LaserCommand(user, argsString);
        }

        [ChatCommand("lc", "Laser control base command S", ChatAuthorizationLevel.User)]
        public static void LaserCommandS(User user, String argsString = "")
        {
            LaserCommand(user, argsString);
        }

        public static void LaserCommand(User user, String argsString = "")
        {
            user.Player.SendTemporaryMessage(LaserControlMod.coloredPrefix+ ChatFormat.UnderLine.Value + ChatFormat.Bold.Value+ChatFormat.Green.Value + "Actual configs (change when meteor has been destroyed):");
            user.Player.SendTemporaryMessage(LaserControlMod.coloredPrefix + ChatFormat.Yellow.Value + "Laser needed: "+LaserControlMod.config.getLaserNeeded());
            user.Player.SendTemporaryMessage(LaserControlMod.coloredPrefix + ChatFormat.Yellow.Value + "Energy per laser needed: " + LaserControlMod.config.getEnergyNeededForLaser());
            user.Player.SendTemporaryMessage(LaserControlMod.coloredPrefix + ChatFormat.Yellow.Value + "Online player needed: " + LaserControlMod.config.onlinePlayersNeededForLaser);

            if (LaserControlMod.config.activateEconomyReward)
            {
                user.Player.SendTemporaryMessage(LaserControlMod.coloredPrefix + ChatFormat.Yellow.Value + "Money per player activator " + LaserControlMod.config.moneyToGivePerPlayer);
                user.Player.SendTemporaryMessage(LaserControlMod.coloredPrefix + ChatFormat.Yellow.Value + "Total money to share with player activator " + LaserControlMod.config.moneyToGiveToShare);

                user.Player.SendTemporaryMessage(LaserControlMod.coloredPrefix + ChatFormat.Yellow.Value + "Money type to give: " + LaserControlMod.config.moneyName);
            }
            else
            {
                user.Player.SendTemporaryMessage(LaserControlMod.coloredPrefix + ChatFormat.Yellow.Value + "No economic reward set");
            }

            if(LaserControlMod.config.activateSkillPointReward)
            {
                user.Player.SendTemporaryMessage(LaserControlMod.coloredPrefix + ChatFormat.Yellow.Value + "Skill point per player activator " + LaserControlMod.config.skillPointRewardPerPlayer);
                user.Player.SendTemporaryMessage(LaserControlMod.coloredPrefix + ChatFormat.Yellow.Value + "Total skill point to share with player activator " + LaserControlMod.config.skillPointRewardToShare);
            }
            else
            {
                user.Player.SendTemporaryMessage(LaserControlMod.coloredPrefix + ChatFormat.Yellow.Value + "No skill point reward set");
            }




        }


    }
}
