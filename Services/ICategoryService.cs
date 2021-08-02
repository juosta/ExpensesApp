using ExpensesApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesApp.Services
{
    public interface ICategoryService
    {
        public Task<List<SelectListItem>> GetCategoryListAsync(Guid userId);
        public Task<int> AddUpdate(CategoryVM model);
        public Task<List<CategoryVM>> GetAllCategories(Guid userId);
        public Task<CategoryVM> GetById(Guid id);
        public Task<int> Delete(Guid id);
    }
}
