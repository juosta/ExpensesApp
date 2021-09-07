using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesApp.Models.ViewModels
{
    public class TransactionListVM
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<TransactionVM> Transactions { get; set; }
    }
}
