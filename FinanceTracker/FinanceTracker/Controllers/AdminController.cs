using FinanceTracker.Models;
using FinanceTracker.Service.Implementations;
using FinanceTracker.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService) =>
            (_adminService) = (adminService);

        public async Task<IActionResult> Index()
        {
            return await GetFullInfoAndShowIndex();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string login)
        {
            var response = await _adminService.CreateNewAdmin(login);

            if (!response.Success)
            {
                ModelState.AddModelError("Error", response.Message);

                return await GetFullInfoAndShowIndex();
            }

            return Redirect("/Admin");
        }

        private async Task<IActionResult> GetFullInfoAndShowIndex()
        {
            var viewModel = new AdminInfoViewModel();

            var getAverageAge = await _adminService.GetAverageAge();
            if (!getAverageAge.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = getAverageAge.Message });
            }
            viewModel.AverageAge = getAverageAge.Data;

            var getAverageIncome = await _adminService.GetAverageIncome();
            if (!getAverageIncome.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = getAverageIncome.Message });
            }
            viewModel.AverageIncome = getAverageIncome.Data;

            var getAverageExpense = await _adminService.GetAverageExpense();
            if (!getAverageExpense.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = getAverageExpense.Message });
            }
            viewModel.AverageExpense = getAverageExpense.Data;


            var getMinAge = await _adminService.GetMinAge();
            if (!getMinAge.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = getMinAge.Message });
            }
            viewModel.MinAge = getMinAge.Data;

            var getMinIncome = await _adminService.GetMinIncome();
            if (!getMinIncome.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = getMinIncome.Message });
            }
            viewModel.MinIncome = getMinIncome.Data;

            var getMinExpense = await _adminService.GetMinExpense();
            if (!getMinExpense.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = getMinExpense.Message });
            }
            viewModel.MinExpense = getMinExpense.Data;


            var getMaxAge = await _adminService.GetMaxAge();
            if (!getMaxAge.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = getMaxAge.Message });
            }
            viewModel.MaxAge = getMaxAge.Data;

            var getMaxIncome = await _adminService.GetMaxIncome();
            if (!getMaxIncome.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = getMaxIncome.Message });
            }
            viewModel.MaxIncome = getMaxIncome.Data;

            var getMaxExpense = await _adminService.GetMaxExpense();
            if (!getMaxExpense.Success)
            {
                return View("Error", new ErrorViewModel() { RequestId = getMaxExpense.Message });
            }
            viewModel.MaxExpense = getMaxExpense.Data;

            return View("Index", viewModel);
        }
    }
}
