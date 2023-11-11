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

            return View(financialAccountServiceResponse.Data.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid financialAccountId)
        {
            var deleteResponse = await _financialAccountService.DeleteFinancialAccountAsync(financialAccountId);

            if (!deleteResponse.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = deleteResponse.Message });
            }

            return Redirect("/FinancialAccount");
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

            var getResponse = await _financialAccountService.GetFinancialAccountsByUserIdAsync(userIdGuid);
            if (!getResponse.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = getResponse.Message });
            }

            if (!ModelState.IsValid)
            {
                return View("Index", getResponse.Data.ToList());
            }

            var updateResponse = await _financialAccountService.UpdateFinancialAccountAsync(model, financialAccountId, userIdGuid);

            if (!updateResponse.Success)
            {
                ModelState.AddModelError("Error", updateResponse.Message);

                return View("Index", getResponse.Data.ToList());
            }

            return Redirect("/FinancialAccount");
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

            var getResponse = await _financialAccountService.GetFinancialAccountsByUserIdAsync(userIdGuid);
            if (!getResponse.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = getResponse.Message });
            }

            if (!ModelState.IsValid)
            {
                return View("Index", getResponse.Data.ToList());
            }

            var createResponse = await _financialAccountService.CreateFinancialAccountAsync(model, userIdGuid);

            if (!createResponse.Success)
            {
                ModelState.AddModelError("Error", createResponse.Message);

                return View("Index", getResponse.Data.ToList());
            }

            return Redirect("/FinancialAccount");
        }

        [HttpPost]
        public IActionResult ClearModelState()
        {
            ViewData.ModelState.Clear();
            return Ok();
        }
    }
}
