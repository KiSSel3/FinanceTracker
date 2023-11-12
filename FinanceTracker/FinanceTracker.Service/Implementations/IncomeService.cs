using FinanceTracker.Domain.Helpers;
using FinanceTracker.Domain.Models;
using FinanceTracker.Domain.Response;
using FinanceTracker.Domain.ViewModels;
using FinanceTracker.Repository.Interfaces;
using FinanceTracker.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Service.Implementations
{
    public class IncomeService : IIncomeService
    {
        private readonly IIncomeRepository _incomeRepository;

        public IncomeService(IIncomeRepository incomeRepository) =>
            (_incomeRepository) = (incomeRepository);

        public async Task<BaseResponse<IncomeModel>> CreateIncomeModelAsync(TransactionViewModel model, Guid userId)
        {
            try
            {
                var newIncomeModel = new IncomeModel(userId, model.TypeId, model.FinancialAccountId, model.Description, model.Amount);

                await _incomeRepository.CreateAsync(newIncomeModel);

                return new BaseResponse<IncomeModel>(true, newIncomeModel);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IncomeModel>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<IncomeModel>> DeleteIncomeModelAsync(Guid incomeModelId)
        {
            try
            {
                var incomeModel = await _incomeRepository.GetByIdAsync(incomeModelId);

                if(incomeModel == null)
                {
                    return new BaseResponse<IncomeModel>(false, "No income");
                }

                await _incomeRepository.DeleteAsync(incomeModel);

                return new BaseResponse<IncomeModel>(true, incomeModel);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IncomeModel>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<PaginatedList<IncomeModel>>> GetIncomeModelByFinancialAccountIdAsync(Guid financialAccountId, int pageNow, int pageSize)
        {
            try
            {
                var allIncomeModels = await _incomeRepository.GetByFinancialAccountIdAsync(financialAccountId);

                var count = allIncomeModels.Count();
                if (count == 0)
                {
                    return new BaseResponse<PaginatedList<IncomeModel>>(true, new PaginatedList<IncomeModel>(allIncomeModels.ToList(), 1, 1));
                }

                int totalPages = (int)Math.Ceiling(count / (double)pageSize);

                if (pageNow > totalPages)
                {
                    return new BaseResponse<PaginatedList<IncomeModel>>(false, "No such page");
                }

                var incomeModels = allIncomeModels
                                    .Skip((pageNow - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

                return new BaseResponse<PaginatedList<IncomeModel>>(true, new PaginatedList<IncomeModel>(incomeModels, pageNow, totalPages));
            }
            catch (Exception ex)
            {
                return new BaseResponse<PaginatedList<IncomeModel>>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<PaginatedList<IncomeModel>>> GetIncomeModelByTypeIdAsync(Guid typeId, int pageNow, int pageSize)
        {
            try
            {
                var allIncomeModels = await _incomeRepository.GetByIncomeTypeIdAsync(typeId);

                var count = allIncomeModels.Count();
                if (count == 0)
                {
                    return new BaseResponse<PaginatedList<IncomeModel>>(true, new PaginatedList<IncomeModel>(allIncomeModels.ToList(), 1, 1));
                }

                int totalPages = (int)Math.Ceiling(count / (double)pageSize);

                if (pageNow > totalPages)
                {
                    return new BaseResponse<PaginatedList<IncomeModel>>(false, "No such page");
                }

                var incomeModels = allIncomeModels
                                    .Skip((pageNow - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

                return new BaseResponse<PaginatedList<IncomeModel>>(true, new PaginatedList<IncomeModel>(incomeModels, pageNow, totalPages));
            }
            catch (Exception ex)
            {
                return new BaseResponse<PaginatedList<IncomeModel>>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<PaginatedList<IncomeModel>>> GetIncomeModelByUserIdAsync(Guid userId, int pageNow, int pageSize)
        {
            try
            {
                var allIncomeModels = await _incomeRepository.GetByUserIdAsync(userId);

                var count = allIncomeModels.Count();
                if(count == 0)
                {
                    return new BaseResponse<PaginatedList<IncomeModel>>(true, new PaginatedList<IncomeModel>(allIncomeModels.ToList(), 1, 1));
                }

                int totalPages = (int)Math.Ceiling(count / (double)pageSize);

                if(pageNow > totalPages)
                {
                    return new BaseResponse<PaginatedList<IncomeModel>>(false, "No such page");
                }

                var incomeModels =  allIncomeModels
                                    .Skip((pageNow - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

                return new BaseResponse<PaginatedList<IncomeModel>>(true, new PaginatedList<IncomeModel>(incomeModels, pageNow, totalPages));
            }
            catch (Exception ex)
            {
                return new BaseResponse<PaginatedList<IncomeModel>>(false, ex.Message);
            }
        }
    }
}
