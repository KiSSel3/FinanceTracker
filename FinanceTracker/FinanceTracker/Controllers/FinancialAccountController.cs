using FinanceTracker.Domain.ViewModels;
using FinanceTracker.Models;
using FinanceTracker.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Controllers
{
    [Authorize]
    public class FinancialAccountController : Controller
    {
        private readonly IFinancialAccountService _financialAccountService;

        public FinancialAccountController(IFinancialAccountService financialAccountService) =>
            (_financialAccountService) = (financialAccountService);

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userIdString = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                return View("Error", new ErrorViewModel() { RequestId = "Ошибка авторизации!" });
            }

            var userIdGuid = Guid.Parse(userIdString);

            var financialAccountServiceResponse = await _financialAccountService.GetFinancialAccountsByUserIdAsync(userIdGuid);
            if (!financialAccountServiceResponse.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = financialAccountServiceResponse.Message });
            }

            return View(financialAccountServiceResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid financialAccountId)
        {
            string userIdString = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                return View("Error", new ErrorViewModel() { RequestId = "Ошибка авторизации!" });
            }

            var userIdGuid = Guid.Parse(userIdString);

            var deleteResponse = await _financialAccountService.DeleteFinancialAccountAsync(financialAccountId);

            if (!deleteResponse.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = deleteResponse.Message });
            }

            var getResponse = await _financialAccountService.GetFinancialAccountsByUserIdAsync(userIdGuid);
            if (!getResponse.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = getResponse.Message });
            }

            return View("Index", getResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(TypeViewModel model, Guid financialAccountId)
        {
            string userIdString = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                return View("Error", new ErrorViewModel() { RequestId = "Ошибка авторизации!" });
            }

            var userIdGuid = Guid.Parse(userIdString);

            if (ModelState.IsValid)
            {
                var updateResponse = await _financialAccountService.UpdateFinancialAccountAsync(model, financialAccountId, userIdGuid);

                if (!updateResponse.Success)
                {
                    ModelState.AddModelError("Error", updateResponse.Message);
                }
            }

            var getResponse = await _financialAccountService.GetFinancialAccountsByUserIdAsync(userIdGuid);
            if (!getResponse.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = getResponse.Message });
            }

            return View("Index", getResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TypeViewModel model)
        {
            string userIdString = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                return View("Error", new ErrorViewModel() { RequestId = "Ошибка авторизации!" });
            }

            var userIdGuid = Guid.Parse(userIdString);

            if (ModelState.IsValid)
            {
                var createResponse = await _financialAccountService.CreateFinancialAccountAsync(model, userIdGuid);

                if (!createResponse.Success)
                {
                    ModelState.AddModelError("Error", createResponse.Message);
                }
            }

            var getResponse = await _financialAccountService.GetFinancialAccountsByUserIdAsync(userIdGuid);
            if (!getResponse.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = getResponse.Message });
            }

            return View("Index", getResponse.Data);
        }
    }
}
