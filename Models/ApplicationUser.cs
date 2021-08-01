using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesApp.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Name { get; set; }

        public virtual ICollection<TransactionCategory> Categories { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
