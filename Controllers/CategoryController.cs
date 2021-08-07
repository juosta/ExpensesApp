using ExpensesApp.Models.Enums;
using ExpensesApp.Models.ViewModels;
using ExpensesApp.Services;
using ExpensesApp.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesApp.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var objList = await _categoryService.GetAllCategories(GetUserId().Value);
            return View(objList);
        }

        public IActionResult Create()
        {
            CategoryVM categoryVM = new CategoryVM()
            {
                CategoryTypeDropDown = Helper.GetDropDownItems<CategoryType>()
            };
            return View(categoryVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryVM obj)
        {
            obj.UserId = GetUserId().Value;
            obj.CategoryTypeDropDown = Helper.GetDropDownItems<CategoryType>();
            await _categoryService.AddUpdate(obj);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var categoryVM = await _categoryService.GetById(id);
            return View(categoryVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CategoryVM obj)
        {
            await _categoryService.AddUpdate(obj);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {            
            await _categoryService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
