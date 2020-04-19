using Eco.Gameplay.Economy;
using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Chat;
using Eco.Shared.Localization;
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
                LocStringBuilder locStringBuilder = new LocStringBuilder();
                locStringBuilder.Append("Usage: /givemoney playername moneyname quantity");
                user.Player.SendTemporaryMessage(locStringBuilder.ToLocString());
                return;
            }


            String[] args = argsString.Split(' ');



            User target = UserManager.FindUserByName(args[0]);

            if (target == null)
            {
                LocStringBuilder locStringBuilder = new LocStringBuilder();
                locStringBuilder.Append("Can't find user: " + args[0]);
                user.Player.SendTemporaryMessage(locStringBuilder.ToLocString());
                return;
            }

            Currency currency = EconomyManager.Currency.GetCurrency(args[1]);

            if (currency == null)
            {
                LocStringBuilder locStringBuilder = new LocStringBuilder();
                locStringBuilder.Append("Can't find money: " + args[1]);
                user.Player.SendTemporaryMessage(locStringBuilder.ToLocString());
                return;
            }

            float toGive;
            bool ok = float.TryParse(args[2], out toGive);

            if(!ok)
            {
                LocStringBuilder locStringBuilder = new LocStringBuilder();
                locStringBuilder.Append("Can't convert float: " + args[2]);
                user.Player.SendTemporaryMessage(locStringBuilder.ToLocString());
                return;
            }
            LocStringBuilder locStringBuilder2 = new LocStringBuilder();
            locStringBuilder2.Append($"Gived {toGive} {args[1]} to {target}");
            user.Player.SendTemporaryMessage(locStringBuilder2.ToLocString());
            currency.CreditAccount(target.Name, toGive);

        }
    }


    }
