using ExpensesApp.Models.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpensesApp.Models
{
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; }
        
        public Guid TransactionCategoryId { get; set; }

        public Guid UserId { get; set; }

        public TransactionType Type { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Range(0.0,double.MaxValue,ErrorMessage ="Amunt must be greater than 0.")]
        public double Amount { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual TransactionCategory TransactionCategory { get; set; }

    }
}
