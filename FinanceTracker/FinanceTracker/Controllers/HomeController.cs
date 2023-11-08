using FinanceTracker.Domain.Models;
using FinanceTracker.Models;
using FinanceTracker.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace FinanceTracker.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IFinancialAccountService _financialAccountService;

        public HomeController(ILogger<HomeController> logger, IUserService userService, IFinancialAccountService financialAccountService)
        {
            _logger = logger;
            _userService = userService;
            _financialAccountService = financialAccountService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}