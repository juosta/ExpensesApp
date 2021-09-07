using ExpensesApp.Data;
using ExpensesApp.Models;
using ExpensesApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpensesApp.Models.Enums;

namespace ExpensesApp.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _db;
        private readonly ICategoryService _categoryService;
        public TransactionService(ApplicationDbContext db, ICategoryService categoryService)
        {
            _db = db;
            _categoryService = categoryService;
        }

        public async Task<List<TransactionVM>> GetAll(Guid userId, DateTime filterDateFrom, DateTime filterDateTo)
        {
            return await _db.Transactions.Where(x => x.UserId == userId && x.Date > filterDateFrom && x.Date < filterDateTo).Select(i => new TransactionVM
            {
                Id = i.Id,
                Date = i.Date,
                Title = i.Title,
                Amount = i.Amount,
                TransactionCategoryId = i.TransactionCategoryId,
                Type = i.Type,
                UserId = i.UserId,
                CategoryName = i.TransactionCategory.CategoryName
            }).ToListAsync();      
        }

        public async Task<CreateTransactionVM> GetById(Guid id)
        {
            var transaction = await _db.Transactions.Where(x => x.Id == id).Select(i => new CreateTransactionVM
            {
                Id = i.Id,
                Date = i.Date,
                Title = i.Title,
                Amount = i.Amount,
                TransactionCategoryId = i.TransactionCategoryId,
                Type = i.Type,
                UserId = i.UserId
            }).FirstOrDefaultAsync();
            transaction.CategoryDropDown = await _categoryService.GetCategoryListAsync(transaction.UserId);
            return transaction;
        }

        public async Task<int> AddUpdate(CreateTransactionVM model)
        {
            if (model.Id != null)
            {
                //update
                var transaction = _db.Transactions.FirstOrDefault(x => x.Id == model.Id);
                transaction.TransactionCategoryId = model.TransactionCategoryId;
                transaction.Type = model.Type;
                transaction.Date = model.Date;
                transaction.Title = model.Title;
                transaction.Amount = model.Amount;
                await _db.SaveChangesAsync();
                return 1;
            }
            //create
            if(model.TransactionCategoryId == Guid.Empty)
            {
                model.TransactionCategoryId = _db.TransactionCategories.FirstOrDefault(x => x.Type == CategoryType.Income).Id;
            }
            var newTransaction = new Transaction()
            {
                TransactionCategoryId = model.TransactionCategoryId,
                UserId = model.UserId,
                Type = model.Type,
                Date = model.Date,
                Title = model.Title,
                Amount = model.Amount
            };
            _db.Transactions.Add(newTransaction);
            await _db.SaveChangesAsync();
            return 2;

        }
        public async Task<int> Delete(Guid id)
        {
            var transaction = _db.Transactions.FirstOrDefault(x => x.Id == id);
            if (transaction != null)
            {
                _db.Transactions.Remove(transaction);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }
    }
}
