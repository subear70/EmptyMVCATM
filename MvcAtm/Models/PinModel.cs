using MvcAtm.Models;

namespace MVCCashMachine.Models
{
    public class PinModel : ServerValidatedModel
    {
        public int Pin { get; set; }
        public long CardNumber { get; set; }
        public string ErrorMessage { get; set; }
    }
}