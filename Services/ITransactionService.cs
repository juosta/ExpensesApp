using ExpensesApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesApp.Services
{
    public interface ITransactionService
    {
        public Task<int> AddUpdate(CreateTransactionVM model);
        public Task<List<TransactionVM>> GetAll(Guid userId);
        public Task<CreateTransactionVM> GetById(Guid id);
    }
}
