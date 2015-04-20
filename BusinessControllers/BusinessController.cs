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
            return true;
        }

        public bool CheckPinNumber(long cardNumber, int pin)
        {
            return true;
        }

        public long GetCardBalance(long cardNumber)
        {
            return 100500;
        }

        public void WithdrawMoney(long cardNumber, long ammount)
        {
        }
    }
}
