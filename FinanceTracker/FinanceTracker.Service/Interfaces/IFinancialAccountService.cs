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
    public interface IFinancialAccountService
    {
        public Task<BaseResponse<IEnumerable<FinancialAccountModel>>> GetFinancialAccountsByUserIdAsync(Guid id);
        public Task<BaseResponse<decimal>> GetTotalAmountByUserIdAsync(Guid id);
        public Task<BaseResponse<decimal>> GetTotalIncomeAmountByUserIdAsync(Guid id);
        public Task<BaseResponse<decimal>> GetTotalExpenseAmountByUserIdAsync(Guid id);
        public Task<BaseResponse<FinancialAccountModel>> CreateFinancialAccountAsync(TypeViewModel model, Guid userId);
        public Task<BaseResponse<FinancialAccountModel>> DeleteFinancialAccountAsync(Guid financialAccountId);
        public Task<BaseResponse<FinancialAccountModel>> UpdateFinancialAccountAsync(TypeViewModel model, Guid financialAccountId, Guid userId);
    }
}
