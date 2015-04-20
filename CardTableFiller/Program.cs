using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Entities;

namespace CardTableFiller
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new ATMContext())
            {
                var getCards = FillCardNumbers();
                dbContext.Cards.AddRange(getCards);
                dbContext.SaveChanges();
                var cards = dbContext.Cards.ToList();
            }
        }

        private static IList<CardInfo> FillCardNumbers()
        {
            var retVal = new List<CardInfo>();
            var stringBuilder = new StringBuilder();
            for (int i = 1; i < 10; i++)
            {
                stringBuilder.Clear();
                for (int j = 0; j < 16; j++)
                {
                    stringBuilder.Append(i);
                }
                var number = stringBuilder.ToString();
                stringBuilder.Clear();
                for (int j = 0; j < 4; j++)
                {
                    stringBuilder.Append(i);
                }
                var pinCode = stringBuilder.ToString();
                var cardInfo = new CardInfo();
                cardInfo.Active = true;
                cardInfo.Balance = (10 - i)*1000;

                cardInfo.CardNumber = GetMD5String(number);
                cardInfo.PinCode = GetMD5String(pinCode);
                retVal.Add(cardInfo);
            }
            return retVal;
        }

        private static string GetMD5String(string number) //TODO: I know it is code duplication, used just for testing purposes
        {
            var tmpSource = ASCIIEncoding.ASCII.GetBytes(number);
            var numberHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            return BitConverter.ToString(numberHash).Replace("-", ""); ;
        }
    }
}
