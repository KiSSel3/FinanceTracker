using FinanceTracker.Domain.ViewModels;
using FinanceTracker.Models;
using FinanceTracker.Service.Implementations;
using FinanceTracker.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinanceTracker.Controllers
{
    [Authorize]
    public class ExpenseController : Controller
    {
        private readonly IExpenseTypeService _expenseTypeService;
        private readonly IExpenseService _expenseService;

        private readonly IFinancialAccountService _financialAccountService;

        private readonly IConfiguration _configuration;
        public ExpenseController(IExpenseTypeService expenseTypeService, IExpenseService expenseService, IFinancialAccountService financialAccountService, IConfiguration configuration) =>
            (_expenseTypeService, _expenseService, _financialAccountService, _configuration) = (expenseTypeService, expenseService, financialAccountService, configuration);

        [HttpGet]
        public async Task<IActionResult> Index(int pageNow = 1)
        {
            string userIdString = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                return View("Error", new ErrorViewModel() { RequestId = "Ошибка авторизации!" });
            }

            var userIdGuid = Guid.Parse(userIdString);

            return await GetFullInfoAndShowIndex(userIdGuid, pageNow);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteType(Guid expenseTypeId)
        {
            var deleteResponse = await _expenseTypeService.DeleteExpenseTypeAsync(expenseTypeId);

            if (!deleteResponse.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = deleteResponse.Message });
            }

            return Redirect("/Expense");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateType(TypeViewModel model, Guid expenseTypeId)
        {
            string userIdString = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                return View("Error", new ErrorViewModel() { RequestId = "Ошибка авторизации!" });
            }

            var userIdGuid = Guid.Parse(userIdString);


            if (!ModelState.IsValid)
            {
                return await GetFullInfoAndShowIndex(userIdGuid);
            }

            var updateResponse = await _expenseTypeService.UpdateExpenseTypeAsync(model, expenseTypeId, userIdGuid);

            if (!updateResponse.Success)
            {
                ModelState.AddModelError("Error", updateResponse.Message);

                return await GetFullInfoAndShowIndex(userIdGuid);
            }

            return Redirect("/Expense");
        }

        [HttpPost]
        public async Task<IActionResult> CreateType(TypeViewModel model)
        {
            string userIdString = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                return View("Error", new ErrorViewModel() { RequestId = "Ошибка авторизации!" });
            }

            var userIdGuid = Guid.Parse(userIdString);

            if (!ModelState.IsValid)
            {
                return await GetFullInfoAndShowIndex(userIdGuid);
            }

            var createResponse = await _expenseTypeService.CreateExpenseTypeAsync(model, userIdGuid);

            if (!createResponse.Success)
            {
                ModelState.AddModelError("Error", createResponse.Message);

                return await GetFullInfoAndShowIndex(userIdGuid);
            }

            return Redirect("/Expense");
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransactionViewModel model)
        {
            string userIdString = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                return View("Error", new ErrorViewModel() { RequestId = "Ошибка авторизации!" });
            }

            var userIdGuid = Guid.Parse(userIdString);

            if (!ModelState.IsValid)
            {
                return await GetFullInfoAndShowIndex(userIdGuid);
            }

            var createResponse = await _expenseService.CreateExpenseModelAsync(model, userIdGuid);

            if (!createResponse.Success)
            {
                ModelState.AddModelError("Error", createResponse.Message);

                return await GetFullInfoAndShowIndex(userIdGuid);
            }

            return Redirect("/Expense");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid expenseId)
        {
            var deleteResponse = await _expenseService.DeleteExpenseModelAsync(expenseId);

            if (!deleteResponse.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = deleteResponse.Message });
            }

            return Redirect("/Expense");
        }

        [HttpPost]
        public async Task<IActionResult> History(Guid expenseTypeId, int? month, int? year)
        {
            string userIdString = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                return View("Error", new ErrorViewModel() { RequestId = "Ошибка авторизации!" });
            }

            var userIdGuid = Guid.Parse(userIdString);

            var response = await _expenseService.GetExpenseModelHistoryAsync(expenseTypeId, month, year);
            if (!response.Success)
            {
                ModelState.AddModelError("Error", response.Message);

                return await GetFullInfoAndShowIndex(userIdGuid);
            }

            return View(response.Data.ToList());
        }

        private async Task<IActionResult> GetFullInfoAndShowIndex(Guid userId, int pageNow = 1)
        {
            var viewModel = new ExpensePageInfoViewModel();

            var getExpenseTypesResponse = await _expenseTypeService.GetExpenseTypesByUserIdAsync(userId);
            if (!getExpenseTypesResponse.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = getExpenseTypesResponse.Message });
            }
            viewModel.ExpenseTypes = getExpenseTypesResponse.Data.ToList();
            viewModel.SelectListExpenseType = new SelectList(getExpenseTypesResponse.Data.ToList(), "Id", "Name");

            var getFinancialAccountResponse = await _financialAccountService.GetFinancialAccountsByUserIdAsync(userId);
            if (!getFinancialAccountResponse.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = getFinancialAccountResponse.Message });
            }
            viewModel.SelectListFinancialAccount = new SelectList(getFinancialAccountResponse.Data.ToList(), "Id", "Name");
            int itemsPerPage = int.Parse(_configuration.GetSection("ItemsPerPage").Value);

            var getExpenseResponse = await _expenseService.GetExpenseModelByUserIdAsync(userId, pageNow, itemsPerPage);
            if (!getExpenseResponse.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = getExpenseResponse.Message });
            }
            viewModel.Expenses = getExpenseResponse.Data;

            return View("Index", viewModel);
        }
    }
}
