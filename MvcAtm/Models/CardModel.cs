using MvcAtm.Models;

namespace MVCCashMachine.Models
{
    public class CardModel : ServerValidatedModel
    {
        public long CardNumber { get; set; }
        public string ErrorMessage { get; set; }
    }
}