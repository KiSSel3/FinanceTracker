using FinanceTracker.Domain.Helpers;
using FinanceTracker.Domain.Models;
using FinanceTracker.Domain.Response;
using FinanceTracker.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Service.Interfaces
{
    public interface IIncomeService
    {
        public Task<BaseResponse<PaginatedList<IncomeModel>>> GetIncomeModelByUserIdAsync(Guid userId, int pageNow, int pageSize);
        public Task<BaseResponse<PaginatedList<IncomeModel>>> GetIncomeModelByTypeIdAsync(Guid typeId, int pageNow, int pageSize);
        public Task<BaseResponse<PaginatedList<IncomeModel>>> GetIncomeModelByFinancialAccountIdAsync(Guid financialAccountId, int pageNow, int pageSize);
        public Task<BaseResponse<IEnumerable<IncomeModel>>> GetIncomeModelHistoryAsync(Guid typeId, int? month, int? year);
        public Task<BaseResponse<IncomeModel>> CreateIncomeModelAsync(TransactionViewModel model, Guid userId);
        public Task<BaseResponse<IncomeModel>> DeleteIncomeModelAsync(Guid incomeModelId);
    }
}
