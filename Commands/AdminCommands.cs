using Eco.Gameplay.Economy;
using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Chat;
using Eco.Gameplay.Items;
using Eco.Shared.Localization;
using System;
using System.Linq;


namespace LaserControl.Commands
{
    public class AdminCommands : IChatCommandHandler
    {
        [ChatCommand("givemoney", "give money to player", ChatAuthorizationLevel.Admin)]
        public static void LaserCommandL(User user, String targetName, String currencyName, float toGive = 200)
        {
            LocStringBuilder locStringBuilder = new LocStringBuilder();
            //check first by user ID

            User target = UserManager.FindUserByName(targetName);

            if (target == null)
            {
                locStringBuilder.Clear();
                locStringBuilder.Append("Can't find user: " + targetName);
                user.Player.SendTemporaryMessage(locStringBuilder.ToLocString());
                return;
            }

            Currency currency = CurrencyManager.Obj.GetCurrency(currencyName);

            if (currency == null)
            {
                locStringBuilder.Clear();
                locStringBuilder.Append("Can't find money: " + currencyName);
                user.Player.SendTemporaryMessage(locStringBuilder.ToLocString());
                return;
            }

           //test ammount to give

            locStringBuilder.Clear();
            locStringBuilder.Append("Gived {toGive} {args[1]} to {target}");
            user.Player.SendTemporaryMessage(locStringBuilder.ToLocString());
            // convert string name to user obj  
            
             target.BankAccount.AddCurrency(currency, toGive);
        }
    }
}
