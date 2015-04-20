using System;

namespace MvcAtm.Models
{
    public class WithdrawalResultModel
    {
        public long CardNumber { get; set; }
        public DateTime DateTime { get; set; }
        public long Amount { get; set; }
        public long Balance { get; set; }
    }
}