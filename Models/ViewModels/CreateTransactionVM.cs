using ExpensesApp.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than 0.")]
        public double Amount { get; set; }

        public IEnumerable<SelectListItem> CategoryDropDown { get; set; }
    }
}
