namespace MVCCashMachine.Models
{
    public class PinModel
    {
        public int Pin { get; set; }
        public long CardNumber { get; set; }
        public string ErrorMessage { get; set; }
    }
}