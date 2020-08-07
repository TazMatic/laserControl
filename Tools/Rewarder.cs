using Eco.Gameplay.Economy;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using System;
using System.Collections;


namespace LaserControl.Tools
{
    public class Rewarder
    {
        public static void reward(ArrayList users)
        {
            if (LaserControlMod.config.activateSkillPointReward)
            {
                int togive = 0;

                if (LaserControlMod.config.skillPointRewardPerPlayer > 0)
                {
                    togive += LaserControlMod.config.skillPointRewardPerPlayer;
                }

                if (LaserControlMod.config.skillPointRewardToShare > 0)
                {
                    int i = LaserControlMod.config.skillPointRewardToShare / users.Count;
                    togive += i;
                }

                if (togive > 0)
                {
                    foreach (User u in users)
                    {
                        u.UseXP(-togive);
                        LocStringBuilder locStringBuilder = new LocStringBuilder();
                        locStringBuilder.Append(LaserControlMod.coloredPrefix + "You win " + togive + " skill points!");
                        u.Player.SendTemporaryMessage(locStringBuilder.ToLocString());
                    }
                }

            }


            if (LaserControlMod.config.activateEconomyReward)
            {
                String money = LaserControlMod.config.moneyName;
                // IDK if this is right. Might want to create a sample mod to test this
                CurrencyManager mgr = new CurrencyManager();
                Currency currency = mgr.GetCurrency(money);

                if (currency == null)
                {
                    Console.WriteLine(LaserControlMod.prefix+"ERROR while rewarding player! Cannot find currency with name: " + money);
                    return;
                }

                int togive = 0;

                if (LaserControlMod.config.moneyToGivePerPlayer > 0)
                {
                    togive += LaserControlMod.config.moneyToGivePerPlayer;
                }

                if (LaserControlMod.config.moneyToGiveToShare > 0)
                {
                    int i = LaserControlMod.config.moneyToGiveToShare / users.Count;
                    togive += i;
                }


                if (togive > 0)
                {
                    foreach (User u in users)
                    {
                        // convert string name to user obj
                        User user = UserManager.FindUserByName(u.Name);
                        BankAccountManager bam = new BankAccountManager();
                        bam.SpawnMoney(currency, user, togive);
                        LocStringBuilder locStringBuilder = new LocStringBuilder();
                        locStringBuilder.Append(LaserControlMod.coloredPrefix + " You win " + togive + " " + money);
                        u.Player.SendTemporaryMessage(locStringBuilder.ToLocString());
                    }
                }
            }
        }
    }
}
