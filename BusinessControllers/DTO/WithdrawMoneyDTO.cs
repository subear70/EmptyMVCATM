using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessControllers.DTO
{
    public class WithdrawMoneyDTO
    {
        public bool Successfull { get; set; }
        public long Balance { get; set; }
    }
}
