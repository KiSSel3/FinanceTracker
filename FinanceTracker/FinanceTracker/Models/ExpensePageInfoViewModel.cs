using FinanceTracker.Domain.Helpers;
using FinanceTracker.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinanceTracker.Models
{
    public class ExpensePageInfoViewModel
    {
        public List<ExpenseTypeModel> ExpenseTypes { get; set; }
        public SelectList SelectListExpenseType { get; set; }
        public SelectList SelectListFinancialAccount { get; set; }
        public PaginatedList<ExpenseModel> Expenses { get; set; }
    }
}
