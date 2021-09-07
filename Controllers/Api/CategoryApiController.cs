using ExpensesApp.Models.ViewModels;
using ExpensesApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesApp.Controllers.Api
{
    [Route("api/Category")]
    [ApiController]
    public class CategoryApiController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryApiController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("GetTransactionsCount")]
        public async Task<int> GetTransactionsCount(Guid id)
        {
            return await _categoryService.GetTransactionCountById(id);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<int> Delete(Guid id, Guid? categoryIdToChange)
        {
            return await _categoryService.Delete(id, categoryIdToChange);
        }
    }
}
