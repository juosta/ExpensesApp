using ExpensesApp.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ExpensesApp.Models.ViewModels
{
    public class CreateTransactionVM
    {
        public Guid? Id { get; set; }
        [DisplayName("Expense Category")]
        public Guid TransactionCategoryId { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public Guid UserId { get; set; }
        public TransactionType Type { get; set; }
        public double Amount { get; set; }

        public IEnumerable<SelectListItem> CategoryDropDown { get; set; }
    }
}
