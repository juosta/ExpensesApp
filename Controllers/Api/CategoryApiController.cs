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
    public class CategoryApiController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly ITransactionService _transactionService;
        public CategoryApiController(ICategoryService categoryService, ITransactionService transactionService)
        {
            _categoryService = categoryService;
            _transactionService = transactionService;
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

        [HttpGet]
        [Route("GetCategoriesData")]
        public async Task<List<TransactionAmountByCategoryVM>> GetCategoriesData(DateTime? filterDateFrom, DateTime? filterDateTo)
        {
            var userId = GetUserId();

            DateTime date = DateTime.Now;
            filterDateFrom ??= new DateTime(date.Year, date.Month, 1); //firstDayOfMonth
            filterDateTo ??= filterDateFrom.Value.AddMonths(1).AddDays(-1); //lastDayOfMonth

            var categories = await _categoryService.GetAllCategories(userId.Value);
            var transactions = await _transactionService.GetAll(GetUserId().Value, filterDateFrom.Value, filterDateTo.Value);
            List<TransactionAmountByCategoryVM> result = new();
            foreach (var category in categories)
            {
                var name = category.CategoryName;
                var sum = transactions.Where(x => x.CategoryName == category.CategoryName).ToList().Sum(i => i.Amount);
                result.Add(new TransactionAmountByCategoryVM() 
                { 
                    Name = category.CategoryName, 
                    Sum = transactions.Where(x => x.CategoryName == category.CategoryName).ToList().Sum(i => i.Amount) 
                });
            }
            return result;
        }
    }
}
