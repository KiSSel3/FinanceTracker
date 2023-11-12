using FinanceTracker.Domain.ViewModels;
using FinanceTracker.Models;
using FinanceTracker.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Controllers
{
    [Authorize]
    public class ExpenseController : Controller
    {
        private readonly IExpenseTypeService _expenseTypeService;
        public ExpenseController(IExpenseTypeService expenseTypeService) =>
            (_expenseTypeService) = (expenseTypeService);

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userIdString = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                return View("Error", new ErrorViewModel() { RequestId = "Ошибка авторизации!" });
            }

            var userIdGuid = Guid.Parse(userIdString);

            var expenseTypeServiceResponse = await _expenseTypeService.GetExpenseTypesByUserIdAsync(userIdGuid);
            if (!expenseTypeServiceResponse.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = expenseTypeServiceResponse.Message });
            }

            return View(expenseTypeServiceResponse.Data.ToList());
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

            var getResponse = await _expenseTypeService.GetExpenseTypesByUserIdAsync(userIdGuid);
            if (!getResponse.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = getResponse.Message });
            }

            if (!ModelState.IsValid)
            {
                return View("Index", getResponse.Data.ToList());
            }

            var updateResponse = await _expenseTypeService.UpdateExpenseTypeAsync(model, expenseTypeId, userIdGuid);

            if (!updateResponse.Success)
            {
                ModelState.AddModelError("Error", updateResponse.Message);

                return View("Index", getResponse.Data.ToList());
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

            var getResponse = await _expenseTypeService.GetExpenseTypesByUserIdAsync(userIdGuid);
            if (!getResponse.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = getResponse.Message });
            }

            if (!ModelState.IsValid)
            {
                return View("Index", getResponse.Data.ToList());
            }

            var createResponse = await _expenseTypeService.CreateExpenseTypeAsync(model, userIdGuid);

            if (!createResponse.Success)
            {
                ModelState.AddModelError("Error", createResponse.Message);

                return View("Index", getResponse.Data.ToList());
            }

            return Redirect("/Expense");
        }
    }
}
