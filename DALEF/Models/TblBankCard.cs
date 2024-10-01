using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALEF.Models
{
    public partial class TblBankCard
    {
        public int BankCardId { get; set; }
        public int OwnerId { get; set; }
        public string Number { get; set; }
        public int CVV { get; set; }
        public int PIN { get; set; }
        public virtual TblUserInfo UserInfo { get; set; } = null!;
    }
}
