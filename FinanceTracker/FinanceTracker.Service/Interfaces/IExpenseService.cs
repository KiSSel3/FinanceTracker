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
    public interface IExpenseService
    {
        public Task<BaseResponse<PaginatedList<ExpenseModel>>> GetExpenseModelByUserIdAsync(Guid userId, int pageNow, int pageSize);
        public Task<BaseResponse<PaginatedList<ExpenseModel>>> GetExpenseModelByTypeIdAsync(Guid typeId, int pageNow, int pageSize);
        public Task<BaseResponse<PaginatedList<ExpenseModel>>> GetExpenseModelByFinancialAccountIdAsync(Guid financialAccountId, int pageNow, int pageSize);
        public Task<BaseResponse<ExpenseModel>> CreateExpenseModelAsync(TransactionViewModel model, Guid userId);
        public Task<BaseResponse<ExpenseModel>> DeleteExpenseModelAsync(Guid expenseModelId);
    }
}
