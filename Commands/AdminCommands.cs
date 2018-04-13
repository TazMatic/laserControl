using Eco.Gameplay.Economy;
using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Chat;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LaserControl.Commands
{
    public class AdminCommands : IChatCommandHandler
    {
        [ChatCommand("givemoney", "give money to player", ChatAuthorizationLevel.Admin)]
        public static void LaserCommandL(User user, String argsString = "")
        {

            if(argsString.Split(' ').Count()<3)
            {
                user.Player.SendTemporaryMessage(FormattableStringFactory.Create("Usage: /givemoney playername moneyname quantity"));
                return;
            }


            String[] args = argsString.Split(' ');



            User target = UserManager.FindUserByName(args[0]);

            if (target == null)
            {
                user.Player.SendTemporaryMessage(FormattableStringFactory.Create("Can't find user: " + args[0]));
                return;
            }

            Currency currency = EconomyManager.Currency.GetCurrency(args[1]);

            if (currency == null)
            {
                user.Player.SendTemporaryMessage(FormattableStringFactory.Create("Can't find money: "+ args[1]));
                return;
            }

            float toGive;
            bool ok = float.TryParse(args[2], out toGive);

            if(!ok)
            {
                user.Player.SendTemporaryMessage(FormattableStringFactory.Create("Can't convert float: " + args[2]));
                return;
            }

            user.Player.SendTemporaryMessage(FormattableStringFactory.Create($"Gived {toGive} {args[1]} to {target}"));
            currency.CreditAccount(target.Name, toGive);

        }
    }


    }
