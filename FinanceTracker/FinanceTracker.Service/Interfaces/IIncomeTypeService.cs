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
    public interface IIncomeTypeService
    {
        public Task<BaseResponse<IEnumerable<IncomeTypeModel>>> GetIncomeTypesByUserIdAsync(Guid id); 
        public Task<BaseResponse<IncomeTypeModel>> CreateIncomeTypeAsync(TypeViewModel model, Guid userId);
        public Task<BaseResponse<IncomeTypeModel>> DeleteIncomeTypeAsync(Guid incomeTypeModelId);
        public Task<BaseResponse<IncomeTypeModel>> UpdateIncomeTypeAsync(TypeViewModel model, Guid incomeTypeModelId, Guid userId);
    }
}
