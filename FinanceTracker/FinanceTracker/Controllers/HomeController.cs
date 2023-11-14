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
        private readonly IIncomeService _incomeService;
        private readonly IExpenseService _expenseService;
        private readonly IFinancialAccountService _financialAccountService;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IIncomeService incomeService, IExpenseService expenseService, IFinancialAccountService financialAccountService) =>
            (_logger, _configuration, _incomeService, _expenseService, _financialAccountService) = (logger, configuration, incomeService, expenseService, financialAccountService);

        public async Task<IActionResult> Index(int expensePageNow = 1, int incomePageNow = 1)
        {
            var viewModel = new HomePageInfoViewModel();

            string userIdString = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                return View("Error", new ErrorViewModel() { RequestId = "Ошибка авторизации!" });
            }

            var userIdGuid = Guid.Parse(userIdString);

            int itemsPerPage = int.Parse(_configuration.GetSection("ItemsPerPage").Value);

            var getExpenseResponse = await _expenseService.GetExpenseModelByUserIdAsync(userIdGuid, expensePageNow, itemsPerPage);
            if (!getExpenseResponse.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = getExpenseResponse.Message });
            }
            viewModel.Expenses = getExpenseResponse.Data;

            var getIncomeResponse = await _incomeService.GetIncomeModelByUserIdAsync(userIdGuid, incomePageNow, itemsPerPage);
            if (!getIncomeResponse.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = getIncomeResponse.Message });
            }
            viewModel.Incomes = getIncomeResponse.Data;

            var financialAccountResponse = await _financialAccountService.GetFinancialAccountsByUserIdAsync(userIdGuid);
            if (!financialAccountResponse.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = financialAccountResponse.Message });
            }
            viewModel.FinancialAccountModels = financialAccountResponse.Data.ToList();

            return View(viewModel);
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}