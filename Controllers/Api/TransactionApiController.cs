using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesApp.Controllers.Api
{
    public class TransactionApiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
