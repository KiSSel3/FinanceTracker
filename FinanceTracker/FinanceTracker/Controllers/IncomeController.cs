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
    public class IncomeController : Controller
    {
        private readonly IIncomeTypeService _incomeTypeService;
        private readonly IIncomeService _incomeService;

        private readonly IFinancialAccountService _financialAccountService;

        private readonly IConfiguration _configuration;
        public IncomeController (IIncomeTypeService incomeTypeService, IIncomeService incomeService, IFinancialAccountService financialAccountService, IConfiguration configuration) =>
            (_incomeTypeService, _incomeService, _financialAccountService, _configuration) = (incomeTypeService, incomeService, financialAccountService, configuration);

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
        public async Task<IActionResult> DeleteType(Guid incomeTypeId)
        {
            var deleteResponse = await _incomeTypeService.DeleteIncomeTypeAsync(incomeTypeId);

            if (!deleteResponse.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = deleteResponse.Message });
            }

            return Redirect("/Income");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateType(TypeViewModel model, Guid incomeTypeId)
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

            var updateResponse = await _incomeTypeService.UpdateIncomeTypeAsync(model, incomeTypeId, userIdGuid);

            if (!updateResponse.Success)
            {
                ModelState.AddModelError("Error", updateResponse.Message);

                return await GetFullInfoAndShowIndex(userIdGuid);
            }

            return Redirect("/Income");
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

            var createResponse = await _incomeTypeService.CreateIncomeTypeAsync(model, userIdGuid);

            if (!createResponse.Success)
            {
                ModelState.AddModelError("Error", createResponse.Message);

                return await GetFullInfoAndShowIndex(userIdGuid);
            }

            return Redirect("/Income");
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

            var createResponse = await _incomeService.CreateIncomeModelAsync(model, userIdGuid);

            if (!createResponse.Success)
            {
                ModelState.AddModelError("Error", createResponse.Message);

                return await GetFullInfoAndShowIndex(userIdGuid);
            }

            return Redirect("/Income");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid incomeId)
        {
            var deleteResponse = await _incomeService.DeleteIncomeModelAsync(incomeId);

            if (!deleteResponse.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = deleteResponse.Message });
            }

            return Redirect("/Income");
        }

        [HttpPost]
        public async Task<IActionResult> History(Guid incomeTypeId, int? month, int? year)
        {
            string userIdString = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                return View("Error", new ErrorViewModel() { RequestId = "Ошибка авторизации!" });
            }
            var userIdGuid = Guid.Parse(userIdString);

            var response = await _incomeService.GetIncomeModelHistoryAsync(incomeTypeId, month, year);
            if (!response.Success)
            {
                ModelState.AddModelError("Error", response.Message);

                return await GetFullInfoAndShowIndex(userIdGuid);
            }

            return View(response.Data.ToList());
        }

        private async Task<IActionResult> GetFullInfoAndShowIndex(Guid userId, int pageNow = 1)
        {
            var viewModel = new IncomePageInfoViewModel();

            var getIncomeTypesResponse = await _incomeTypeService.GetIncomeTypesByUserIdAsync(userId);
            if (!getIncomeTypesResponse.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = getIncomeTypesResponse.Message });
            }
            viewModel.IncomeTypes = getIncomeTypesResponse.Data.ToList();
            viewModel.SelectListIncomeType = new SelectList(getIncomeTypesResponse.Data.ToList(), "Id", "Name");

            var getFinancialAccountResponse = await _financialAccountService.GetFinancialAccountsByUserIdAsync(userId);
            if (!getFinancialAccountResponse.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = getFinancialAccountResponse.Message });
            }
            viewModel.SelectListFinancialAccount = new SelectList(getFinancialAccountResponse.Data.ToList(), "Id", "Name");

            int itemsPerPage = int.Parse(_configuration.GetSection("ItemsPerPage").Value);

            var getIncomeResponse = await _incomeService.GetIncomeModelByUserIdAsync(userId, pageNow, itemsPerPage);
            if (!getIncomeResponse.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = getIncomeResponse.Message });
            }
            viewModel.Incomes = getIncomeResponse.Data;

            return View("Index", viewModel);
        }
    }
}
