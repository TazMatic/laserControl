using Eco.Gameplay.Economy;
using Eco.Gameplay.Players;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserControl.Tools
{
    public class Rewarder
    {
        public static void reward(ArrayList users)
        {
            if (LaserControl.config.activateSkillPointReward)
            {
                int togive = 0;

                if (LaserControl.config.skillPointRewardPerPlayer > 0)
                {
                    togive += LaserControl.config.skillPointRewardPerPlayer;
                }

                if (LaserControl.config.skillPointRewardToShare > 0)
                {
                    int i = LaserControl.config.skillPointRewardToShare / users.Count;
                    togive += i;
                }

                if (togive > 0)
                {
                    foreach (User u in users)
                    {
                        u.UseXP(-togive);
                        u.Player.SendTemporaryMessage(LaserControl.coloredPrefix + "You win " + togive + " skill point !");
                    }
                }

            }


            if (LaserControl.config.activateEconomyReward)
            {
                String money = LaserControl.config.moneyName;
                Currency currency = EconomyManager.Currency.GetCurrency(money);

                if (currency == null)
                {
                    Console.WriteLine(LaserControl.prefix+"ERROR while rewarding player ! We can't fin this money name: " + money);
                    return;
                }

                int togive = 0;

                if (LaserControl.config.moneyToGivePerPlayer > 0)
                {
                    togive += LaserControl.config.moneyToGivePerPlayer;
                }

                if (LaserControl.config.moneyToGiveToShare > 0)
                {
                    int i = LaserControl.config.moneyToGiveToShare / users.Count;
                    togive += i;
                }


                if (togive > 0)
                {
                    foreach (User u in users)
                    {
                        currency.CreditAccount(u.Name, -50);
                        u.Player.SendTemporaryMessage(LaserControl.coloredPrefix+"You win " + togive + " "+money+" money !");
                    }
                }


            }



        }
    }
}
