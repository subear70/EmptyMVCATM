using System;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BusinessControllers.DTO;
using Data;
using Data.Entities;

namespace BusinessControllers
{
    public class BusinessController //TODO: add handling of BusinessController through interfaces
    {
        private static BusinessController _instance;

        protected BusinessController()
        {
        }

        public static BusinessController Instance
        {
            get { return _instance ?? (_instance = new BusinessController()); }
        }

        public bool CheckCardNumber(long cardNumber)
        {
            using (var dbContext = new ATMContext())
            {
                var cardNumberString = GetMD5String(cardNumber.ToString(CultureInfo.InvariantCulture));
                var activeCardIsPresent = dbContext.Cards.Any(x => x.Active && x.CardNumber == cardNumberString);
                return activeCardIsPresent;
            }
        }

        public CheckPinDTO CheckPinNumber(long cardNumber, int pin, int numberOfTries)
        {
            var retVal = new CheckPinDTO();
            using (var dbContext = new ATMContext())
            {
                var cardNumberString = GetMD5String(cardNumber.ToString(CultureInfo.InvariantCulture));
                var card = dbContext.Cards.First(x => x.Active || x.CardNumber == cardNumberString);
                retVal.Successful = card.PinCode == GetMD5String(pin.ToString(CultureInfo.InvariantCulture));
                if (!retVal.Successful)
                {
                    if (numberOfTries == 3) //TODO: move this magic number to the settings
                    {
                        card.Active = false;
                        retVal.Blocked = true;
                        dbContext.SaveChanges();
                    }
                }
            }
            return retVal;
        }

        public long GetCardBalance(long cardNumber)
        {
            using (var dbContext = new ATMContext())
            {
                var cardNumberString = GetMD5String(cardNumber.ToString(CultureInfo.InvariantCulture));
                var card = dbContext.Cards.First(x => x.Active || x.CardNumber == cardNumberString);
                var logInfo = new LogInfo { CardId = cardNumber, OperationCode = 1 };
                dbContext.Logs.Add(logInfo);
                dbContext.SaveChanges();
                return card.Balance;
            }
        }

        public WithdrawMoneyDTO WithdrawMoney(long cardNumber, long ammount)
        {
            var retVal = new WithdrawMoneyDTO();
            using (var dbContext = new ATMContext())
            {
                var cardNumberString = GetMD5String(cardNumber.ToString(CultureInfo.InvariantCulture));
                var card = dbContext.Cards.First(x => x.Active || x.CardNumber == cardNumberString);
                if (card.Balance >= ammount)
                {
                    card.Balance -= ammount;
                    retVal.Successfull = true;
                    retVal.Balance = card.Balance;
                    var logInfo = new LogInfo {CardId = cardNumber, OperationCode = 0, OperationValue = ammount};
                    dbContext.Logs.Add(logInfo);
                    dbContext.SaveChanges();
                }
            }
            return retVal;
        }

        private static string GetMD5String(string number)
        {
            var tmpSource = ASCIIEncoding.ASCII.GetBytes(number);
            var numberHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            return BitConverter.ToString(numberHash).Replace("-", ""); ;
        }
    }
}
