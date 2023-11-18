using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etw_BANK_FINAL.Model
{
    internal class User
    {

        // User class containing user-related properties

        // Properties for UserId, UserName, and PinCode
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PinCode { get; set; }

        // Navigation property for associated accounts
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
