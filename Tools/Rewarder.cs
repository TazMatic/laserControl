using Eco.Gameplay.Economy;
using Eco.Gameplay.Players;
using System;
using System.Collections;
using System.Runtime.CompilerServices;

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
                        u.Player.SendTemporaryMessage(FormattableStringFactory.Create(LaserControlMod.coloredPrefix + "You win " + togive + " skill point !"));
                    }
                }

            }


            if (LaserControlMod.config.activateEconomyReward)
            {
                String money = LaserControlMod.config.moneyName;
                Currency currency = EconomyManager.Currency.GetCurrency(money);

                if (currency == null)
                {
                    Console.WriteLine(LaserControlMod.prefix+"ERROR while rewarding player ! We can't fin this money name: " + money);
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
                        currency.CreditAccount(u.Name, togive);
                        u.Player.SendTemporaryMessage(FormattableStringFactory.Create(LaserControlMod.coloredPrefix+"You win " + togive + " "+money+" money !"));
                    }
                }


            }



        }
    }
}
