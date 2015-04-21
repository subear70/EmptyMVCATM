using System.ComponentModel.DataAnnotations;
using MvcAtm.Models;

namespace MVCCashMachine.Models
{
    public class PinModel
    {
        public int Pin { get; set; }
        public long CurrentCardNumber { get; set; } 
    }
}