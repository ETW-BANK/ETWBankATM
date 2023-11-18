using Etw_BANK_FINAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etw_BANK_FINAL.Data
{
    internal class EtwBankContext:DbContext
    {
        // Define DbSet properties for User and Account entities
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }

        // Override method to configure database connection
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure your database connection here
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ETWDB_Tensae;Integrated Security=True;Pooling=False");
        }
    }
}
