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

        public async Task<IActionResult> Index()
        {
            var objList = await _transactionService.GetAll(GetUserId().Value);
            return View(objList);
        }
        // Get create
        public async Task<IActionResult> Create()
        {
            var userId = GetUserId();
            TransactionVM transactionVM = new TransactionVM()
            {
                CategoryDropDown = await _categoryService.GetCategoryListAsync(userId.Value)
            };
            return View(transactionVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTransactionVM obj)
        {
            obj.UserId = GetUserId().Value;
            await _transactionService.AddUpdate(obj);
            return RedirectToAction("Index");
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
    }
}
