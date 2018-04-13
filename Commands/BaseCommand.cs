using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Chat;
using EcoColorLib;
using LaserControlLight.Config;
using System;
using System.Runtime.CompilerServices;

namespace LaserControl.Commands
{
    class BaseCommand : IChatCommandHandler
    {
        [ChatCommand("lasercontroldetail", "Laser control base command L", ChatAuthorizationLevel.User)]
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
            user.Player.SendTemporaryMessage(FormattableStringFactory.Create(LaserControlMod.coloredPrefix+ ChatFormat.UnderLine.Value + ChatFormat.Bold.Value+ChatFormat.Green.Value + "Actual configs (change when meteor has been destroyed):"));
            user.Player.SendTemporaryMessage(FormattableStringFactory.Create(LaserControlMod.coloredPrefix + ChatFormat.Yellow.Value + "Laser needed: "+ LaserLightConfig.commonGetter.getLaserNeeded()));
            user.Player.SendTemporaryMessage(FormattableStringFactory.Create(LaserControlMod.coloredPrefix + ChatFormat.Yellow.Value + "Energy per laser needed: " + LaserLightConfig.commonGetter.getEnergyNeededForLaser()));
            user.Player.SendTemporaryMessage(FormattableStringFactory.Create(LaserControlMod.coloredPrefix + ChatFormat.Yellow.Value + "Online player needed: " + LaserControlLight.LaserControlLight.config.onlinePlayersNeededForLaser));

            if (LaserControlMod.config.activateEconomyReward)
            {
                user.Player.SendTemporaryMessage(FormattableStringFactory.Create(LaserControlMod.coloredPrefix + ChatFormat.Yellow.Value + "Money per player activator " + LaserControlMod.config.moneyToGivePerPlayer));
                user.Player.SendTemporaryMessage(FormattableStringFactory.Create(LaserControlMod.coloredPrefix + ChatFormat.Yellow.Value + "Total money to share with player activator " + LaserControlMod.config.moneyToGiveToShare));

                user.Player.SendTemporaryMessage(FormattableStringFactory.Create(LaserControlMod.coloredPrefix + ChatFormat.Yellow.Value + "Money type to give: " + LaserControlMod.config.moneyName));
            }
            else
            {
                user.Player.SendTemporaryMessage(FormattableStringFactory.Create(LaserControlMod.coloredPrefix + ChatFormat.Yellow.Value + "No economic reward set"));
            }

            if(LaserControlMod.config.activateSkillPointReward)
            {
                user.Player.SendTemporaryMessage(FormattableStringFactory.Create(LaserControlMod.coloredPrefix + ChatFormat.Yellow.Value + "Skill point per player activator " + LaserControlMod.config.skillPointRewardPerPlayer));
                user.Player.SendTemporaryMessage(FormattableStringFactory.Create(LaserControlMod.coloredPrefix + ChatFormat.Yellow.Value + "Total skill point to share with player activator " + LaserControlMod.config.skillPointRewardToShare));
            }
            else
            {
                user.Player.SendTemporaryMessage(FormattableStringFactory.Create(LaserControlMod.coloredPrefix + ChatFormat.Yellow.Value + "No skill point reward set"));
            }




        }


    }
}
