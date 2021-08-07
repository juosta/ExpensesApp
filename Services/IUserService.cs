using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesApp.Services
{
    public interface IUserService
    {
        public Task<int> CreateDefaultCategories(Guid id);
    }
}
