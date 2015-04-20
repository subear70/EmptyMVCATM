using System.Net.Mime;

namespace MvcAtm.Models
{
    public class WithdrawalModel : ServerValidatedModel
    {
        public long WithdrawalAmmount { get; set; }
    }
}