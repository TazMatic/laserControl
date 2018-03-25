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
            user.Player.SendTemporaryMessage(LaserControl.coloredPrefix+ ChatFormat.UnderLine.Value + ChatFormat.Bold.Value+ChatFormat.Green.Value + "Actual configs (change when meteor has been destroyed):");
            user.Player.SendTemporaryMessage(LaserControl.coloredPrefix + ChatFormat.Yellow.Value + "Laser needed: "+LaserControl.config.getLaserNeeded());
            user.Player.SendTemporaryMessage(LaserControl.coloredPrefix + ChatFormat.Yellow.Value + "Energy per laser needed: " + LaserControl.config.getEnergyNeededForLaser());
            user.Player.SendTemporaryMessage(LaserControl.coloredPrefix + ChatFormat.Yellow.Value + "Online player needed: " + LaserControl.config.onlinePlayersNeededForLaser);

            if (LaserControl.config.activateEconomyReward)
            {
                user.Player.SendTemporaryMessage(LaserControl.coloredPrefix + ChatFormat.Yellow.Value + "Money per player activator " + LaserControl.config.moneyToGivePerPlayer);
                user.Player.SendTemporaryMessage(LaserControl.coloredPrefix + ChatFormat.Yellow.Value + "Total money to share with player activator " + LaserControl.config.moneyToGiveToShare);

                user.Player.SendTemporaryMessage(LaserControl.coloredPrefix + ChatFormat.Yellow.Value + "Money type to give: " + LaserControl.config.moneyName);
            }
            else
            {
                user.Player.SendTemporaryMessage(LaserControl.coloredPrefix + ChatFormat.Yellow.Value + "No economic reward set");
            }

            if(LaserControl.config.activateSkillPointReward)
            {
                user.Player.SendTemporaryMessage(LaserControl.coloredPrefix + ChatFormat.Yellow.Value + "Skill point per player activator " + LaserControl.config.skillPointRewardPerPlayer);
                user.Player.SendTemporaryMessage(LaserControl.coloredPrefix + ChatFormat.Yellow.Value + "Total skill point to share with player activator " + LaserControl.config.skillPointRewardToShare);
            }
            else
            {
                user.Player.SendTemporaryMessage(LaserControl.coloredPrefix + ChatFormat.Yellow.Value + "No skill point reward set");
            }




        }


    }
}
