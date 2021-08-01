using ExpensesApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesApp.Models
{
    public class TransactionCategory
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Category Name")]
        [Required]
        public string CategoryName { get; set; }

        public CategoryType Type { get; set; }

        public Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
