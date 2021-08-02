using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesApp.Models.Enums
{
    public enum CategoryType
    {
        Must,
        [Display(Name = "Nice to have")]
        NiceToHave,
        Useless
    }
}
