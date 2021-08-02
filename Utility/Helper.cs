using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ExpensesApp.Utility
{
    public static class Helper
    {
        public static List<SelectListItem> GetDropDownItems<TEnum>() where TEnum : Enum
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Select(x => new SelectListItem
            {
                Value = x.ToString(),
                Text = x.GetAttribute<DisplayAttribute>()?.Name ?? x.ToString()
            }).ToList();
        }

        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue) where TAttribute : Attribute
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<TAttribute>();
        }
    }
}
