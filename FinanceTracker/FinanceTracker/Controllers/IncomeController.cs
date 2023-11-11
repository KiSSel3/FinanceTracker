using FinanceTracker.Domain.ViewModels;
using FinanceTracker.Models;
using FinanceTracker.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Controllers
{
    public class IncomeController : Controller
    {
        private readonly IIncomeTypeService _incomeTypeService;
        public IncomeController (IIncomeTypeService incomeTypeService) =>
            (_incomeTypeService) = (incomeTypeService);

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userIdString = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                return View("Error", new ErrorViewModel() { RequestId = "Ошибка авторизации!" });
            }

            var userIdGuid = Guid.Parse(userIdString);

            var incomeTypeServiceResponse = await _incomeTypeService.GetIncomeTypesByUserIdAsync(userIdGuid);
            if (!incomeTypeServiceResponse.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = incomeTypeServiceResponse.Message });
            }

            return View(incomeTypeServiceResponse.Data.ToList());
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

            var getResponse = await _incomeTypeService.GetIncomeTypesByUserIdAsync(userIdGuid);
            if (!getResponse.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = getResponse.Message });
            }

            if (!ModelState.IsValid)
            {
                return View("Index", getResponse.Data.ToList());
            }

            var updateResponse = await _incomeTypeService.UpdateIncomeTypeAsync(model, incomeTypeId, userIdGuid);

            if (!updateResponse.Success)
            {
                ModelState.AddModelError("Error", updateResponse.Message);

                return View("Index", getResponse.Data.ToList());
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

            var getResponse = await _incomeTypeService.GetIncomeTypesByUserIdAsync(userIdGuid);
            if (!getResponse.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = getResponse.Message });
            }

            if (!ModelState.IsValid)
            {
                return View("Index", getResponse.Data.ToList());
            }

            var createResponse = await _incomeTypeService.CreateIncomeTypeAsync(model, userIdGuid);

            if (!createResponse.Success)
            {
                ModelState.AddModelError("Error", createResponse.Message);

                return View("Index", getResponse.Data.ToList());
            }

            return Redirect("/Income");
        }
    }
}
