using ExpensesApp.Models;
using ExpensesApp.Models.ViewModels;
using ExpensesApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpensesApp.Models.Enums;

namespace ExpensesApp.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;
        public UserService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<int> CreateDefaultCategories(Guid id)
        {
            var categories = new List<TransactionCategory> {
                new TransactionCategory{CategoryName = "Salary", UserId = id, Type=CategoryType.Income},
                new TransactionCategory{CategoryName = "Other", UserId = id, Type=CategoryType.Income},
                new TransactionCategory{CategoryName = "Food", UserId = id, Type=CategoryType.Must},
                new TransactionCategory{CategoryName = "Clothes", UserId = id, Type=CategoryType.Must},
                new TransactionCategory{CategoryName = "Utilities", UserId = id, Type=CategoryType.Must},
                new TransactionCategory{CategoryName = "Transport", UserId = id, Type=CategoryType.Must},
                new TransactionCategory{CategoryName = "Health & Beauty", UserId = id, Type=CategoryType.Must},
                new TransactionCategory{CategoryName = "Investment & Saving", UserId = id, Type=CategoryType.NiceToHave},
                new TransactionCategory{CategoryName = "Leisure & Entertainment", UserId = id, Type=CategoryType.NiceToHave},
            };
         
            _db.TransactionCategories.AddRange(categories);
            await _db.SaveChangesAsync();
            return 0;
        }

    }
}
