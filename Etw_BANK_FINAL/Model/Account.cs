using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etw_BANK_FINAL.Model
{
    internal class Account
    {
        // Account class containing Account-related properties

        public int Id { get; set; }


        public int UserId { get; set; }
        public int AccountNumber { get; set; }
        public string AccType { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public DateTime TransactionDate { get; set; }

        // Navigation property for associated User
        public virtual User User { get; set; }
    }
}
