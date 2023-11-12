using FinanceTracker.Models;
using FinanceTracker.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Components.FinancialInfo
{
    public class FinancialInfoViewComponent : ViewComponent
    {
        private readonly IFinancialAccountService _financialAccountService;

        public FinancialInfoViewComponent(IFinancialAccountService financialAccountService) =>
            (_financialAccountService) = (financialAccountService);

        public IViewComponentResult Invoke(Guid userId)
        {
            var viewModel = new FinancialInfoViewModel();

            var getIncomeAmountResponse = _financialAccountService.GetTotalIncomeAmountByUserIdAsync(userId).Result;
            if (getIncomeAmountResponse.Success)
            {
                viewModel.IncomeAmount = getIncomeAmountResponse.Data;
            }

            var getExpenseAmountResponse = _financialAccountService.GetTotalExpenseAmountByUserIdAsync(userId).Result;
            if (getExpenseAmountResponse.Success)
            {
                viewModel.ExpenseAmount = getExpenseAmountResponse.Data;
            }

            viewModel.TotalAmount = viewModel.IncomeAmount - viewModel.ExpenseAmount;

            return View(viewModel);
        }
    }
}
