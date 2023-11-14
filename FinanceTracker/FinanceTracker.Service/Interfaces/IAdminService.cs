using FinanceTracker.Domain.Models;
using FinanceTracker.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Service.Interfaces
{
    public interface IAdminService
    {
        public Task<BaseResponse<int>> GetAverageAge();
        public Task<BaseResponse<int>> GetMaxAge();
        public Task<BaseResponse<int>> GetMinAge();

        public Task<BaseResponse<decimal>> GetAverageExpense();
        public Task<BaseResponse<decimal>> GetMaxExpense();
        public Task<BaseResponse<decimal>> GetMinExpense();

        public Task<BaseResponse<decimal>> GetAverageIncome();
        public Task<BaseResponse<decimal>> GetMaxIncome();
        public Task<BaseResponse<decimal>> GetMinIncome();

        public Task<BaseResponse<UserModel>> CreateNewAdmin(string login);
    }
}
