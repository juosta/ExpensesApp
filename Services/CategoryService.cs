using ExpensesApp.Data;
using ExpensesApp.Models;
using ExpensesApp.Models.Enums;
using ExpensesApp.Models.ViewModels;
using ExpensesApp.Utility;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _db;
        public CategoryService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<SelectListItem>> GetCategoryListAsync (Guid userId) 
        {
            return await _db.TransactionCategories.Where(x => x.UserId == userId).Select(i => new SelectListItem
            {
                Text = i.CategoryName,
                Value = i.Id.ToString()
            }).ToListAsync();
        }
        public async Task<List<CategoryVM>> GetAllCategories(Guid userId)
        {
            return await _db.TransactionCategories.Where(x => x.UserId == userId).Select(i => new CategoryVM
            {
                Id = i.Id,
                CategoryName = i.CategoryName,
                Type = i.Type,
                UserId = i.UserId
            }).ToListAsync();
        }

        public async Task<CategoryVM> GetById(Guid id)
        {
            var category = await _db.TransactionCategories.Where(x => x.Id == id).Select(i => new CategoryVM
            {
                Id = i.Id,
                CategoryName = i.CategoryName,
                Type = i.Type,
                UserId = i.UserId
            }).FirstOrDefaultAsync();
            category.CategoryTypeDropDown = Helper.GetDropDownItems<CategoryType>();
            return category;
        }

        public async Task<int> AddUpdate(CategoryVM model)
        {
            if (model.Id != null)
            {
                //update
                var category = _db.TransactionCategories.FirstOrDefault(x => x.Id == model.Id);
                category.CategoryName = model.CategoryName;
                category.Type = model.Type;
                await _db.SaveChangesAsync();
                return 1;
            }
            //create
            var newCategory = new TransactionCategory()
            {
                CategoryName = model.CategoryName,
                Type = model.Type,
                UserId = model.UserId
            };
            _db.TransactionCategories.Add(newCategory);
            await _db.SaveChangesAsync();
            return 2;

        }

        public async Task<int> Delete(Guid id)
        {
            var category = _db.TransactionCategories.FirstOrDefault(x => x.Id == id);
            if (category != null)
            {
                _db.TransactionCategories.Remove(category);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }
        }
}
