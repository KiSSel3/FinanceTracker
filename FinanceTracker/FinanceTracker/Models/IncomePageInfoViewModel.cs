using FinanceTracker.Domain.Helpers;
using FinanceTracker.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinanceTracker.Models
{
    public class IncomePageInfoViewModel
    {
        public List<IncomeTypeModel> IncomeTypes { get; set; }
        public SelectList SelectListIncomeType { get; set; }
        public SelectList SelectListFinancialAccount { get; set; }
        public PaginatedList<IncomeModel> Incomes { get; set; }
    }
}
