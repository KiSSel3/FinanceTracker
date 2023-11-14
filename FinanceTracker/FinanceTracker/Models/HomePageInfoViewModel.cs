using FinanceTracker.Domain.Helpers;
using FinanceTracker.Domain.Models;

namespace FinanceTracker.Models
{
    public class HomePageInfoViewModel
    {
        public PaginatedList<IncomeModel> Incomes { get; set; }
        public PaginatedList<ExpenseModel> Expenses { get; set; }
        public List<FinancialAccountModel> FinancialAccountModels { get; set; }
    }
}
