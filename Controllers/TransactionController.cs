using ExpensesApp.Models.Enums;
using ExpensesApp.Models.ViewModels;
using ExpensesApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ExpensesApp.Controllers
{
    public class TransactionController : BaseController
    {
        private readonly ITransactionService _transactionService;       
        private readonly ICategoryService _categoryService;

        public TransactionController(ITransactionService transactionService, ICategoryService categoryService) 
        {
            _transactionService = transactionService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(DateTime? filterDateFrom, DateTime? filterDateTo)
        {
            var userId = GetUserId();
            if(userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            DateTime date = DateTime.Now;
            filterDateFrom ??= new DateTime(date.Year, date.Month, 1); //firstDayOfMonth
            filterDateTo ??= filterDateFrom.Value.AddMonths(1).AddDays(-1); //lastDayOfMonth

            var result = new TransactionListVM()
            {
                DateFrom = filterDateFrom.Value,
                DateTo = filterDateTo.Value,
                Transactions = await _transactionService.GetAll(GetUserId().Value, filterDateFrom.Value, filterDateTo.Value)
            };
            return View(result);
        }
        // Get create
        public async Task<IActionResult> Create(TransactionType type)
        {
            var userId = GetUserId();
            var transactionVM = new CreateTransactionVM()
            {
                Type = type,
                CategoryDropDown = await _categoryService.GetCategoryListAsync(userId.Value)
            };
            return View(transactionVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTransactionVM obj)
        {
            obj.UserId = GetUserId().Value;
            obj.CategoryDropDown = await _categoryService.GetCategoryListAsync(obj.UserId);
            if (!ModelState.IsValid)
            {
                //handle error
            }
            else
            {
                await _transactionService.AddUpdate(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var transactionVM = await _transactionService.GetById(id);
            return View(transactionVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CreateTransactionVM obj)
        {
            await _transactionService.AddUpdate(obj);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _transactionService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
