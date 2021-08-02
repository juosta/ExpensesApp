using ExpensesApp.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesApp.Models.ViewModels
{
    public class CategoryVM
    {
        public Guid? Id { get; set; }

        [DisplayName("Category Name")]
        [Required]
        public string CategoryName { get; set; }

        public CategoryType Type { get; set; }

        public Guid UserId { get; set; }

        public IEnumerable<SelectListItem> CategoryTypeDropDown { get; set; }
    }
}
