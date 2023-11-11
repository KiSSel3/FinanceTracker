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
    public interface IExpenseTypeService
    {
        public Task<BaseResponse<IEnumerable<ExpenseTypeModel>>> GetExpenseTypesByUserIdAsync(Guid id);
        public Task<BaseResponse<ExpenseTypeModel>> CreateExpenseTypeAsync(TypeViewModel model, Guid userId);
        public Task<BaseResponse<ExpenseTypeModel>> DeleteExpenseTypeAsync(Guid expenseTypeId);
        public Task<BaseResponse<ExpenseTypeModel>> UpdateExpenseTypeAsync(TypeViewModel model, Guid expenseTypeId, Guid userId);
    }
}
