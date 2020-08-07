using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Chat;
using Eco.Shared.Localization;
using EcoColorLib;
using LaserControl.Config;
using System;


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
            LocStringBuilder locStringBuilder = new LocStringBuilder();
            locStringBuilder.Append(LaserControlMod.coloredPrefix + ChatFormat.UnderLine.Value + ChatFormat.Bold.Value + ChatFormat.Green.Value + "Actual configs (change when meteor has been destroyed):");
            user.Player.SendTemporaryMessage(locStringBuilder.ToLocString());
            locStringBuilder.Clear();
            ConfigGetter config = new ConfigGetter();
            locStringBuilder.Append(LaserControlMod.coloredPrefix + ChatFormat.Yellow.Value + "Laser needed: " + config.getLaserNeeded());
            user.Player.SendTemporaryMessage(locStringBuilder.ToLocString());
            //Write get energy needed and player ammount getter 
            //user.Player.SendTemporaryMessage(FormattableStringFactory.Create(LaserControlMod.coloredPrefix + ChatFormat.Yellow.Value + "Energy per laser needed: " + config.getEnergyNeededForLaser()));
            //user.Player.SendTemporaryMessage(FormattableStringFactory.Create(LaserControlMod.coloredPrefix + ChatFormat.Yellow.Value + "Online player needed: " + LaserControl.LaserControl.config.onlinePlayersNeededForLaser));

            if (LaserControlMod.config.activateEconomyReward)
            {
                //user.Player.SendTemporaryMessage(FormattableStringFactory.Create(LaserControlMod.coloredPrefix + ChatFormat.Yellow.Value + "Money per player activator " + LaserControlMod.config.moneyToGivePerPlayer));
                //user.Player.SendTemporaryMessage(FormattableStringFactory.Create(LaserControlMod.coloredPrefix + ChatFormat.Yellow.Value + "Total money to share with player activator " + LaserControlMod.config.moneyToGiveToShare));

                //user.Player.SendTemporaryMessage(FormattableStringFactory.Create(LaserControlMod.coloredPrefix + ChatFormat.Yellow.Value + "Money type to give: " + LaserControlMod.config.moneyName));
                //TMP for testing
                locStringBuilder.Clear();
                locStringBuilder.Append("ECO REWARD SET");
                user.Player.SendTemporaryMessage(locStringBuilder.ToLocString());
            }
            else
            {
                locStringBuilder.Clear();
                locStringBuilder.Append(LaserControlMod.coloredPrefix + ChatFormat.Yellow.Value + "No economic reward set");
                user.Player.SendTemporaryMessage(locStringBuilder.ToLocString());
            }

            if(LaserControlMod.config.activateSkillPointReward)
            {
                locStringBuilder.Clear();
                locStringBuilder.Append(LaserControlMod.coloredPrefix + ChatFormat.Yellow.Value + "Skill point per player activator " + LaserControlMod.config.skillPointRewardPerPlayer);
                user.Player.SendTemporaryMessage(locStringBuilder.ToLocString());
                locStringBuilder.Clear();
                locStringBuilder.Append(LaserControlMod.coloredPrefix + ChatFormat.Yellow.Value + "Total skill point to share with player activator " + LaserControlMod.config.skillPointRewardToShare);
                user.Player.SendTemporaryMessage(locStringBuilder.ToLocString());
            }
            else
            {
                locStringBuilder.Clear();
                locStringBuilder.Append(LaserControlMod.coloredPrefix + ChatFormat.Yellow.Value + "No skill point reward set");
                user.Player.SendTemporaryMessage(locStringBuilder.ToLocString());
            }




        }


    }
}
