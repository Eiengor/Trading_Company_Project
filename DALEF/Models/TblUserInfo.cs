using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALEF.Models
{
    public partial class TblUserInfo
    {
        public int UserId { get; set; }
        public string UserLogin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HashPassword { get; set; }
        public string PasswordKeyword { get; set; }
        public string Gender { get; set; }
        public string UserAddress { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public virtual ICollection<TblBankCard> TblBankCards { get; set; } = new List<TblBankCard>;
    }
}
