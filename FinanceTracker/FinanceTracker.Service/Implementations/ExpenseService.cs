using FinanceTracker.Domain.Helpers;
using FinanceTracker.Domain.Models;
using FinanceTracker.Domain.Response;
using FinanceTracker.Domain.ViewModels;
using FinanceTracker.Repository.Implementations;
using FinanceTracker.Repository.Interfaces;
using FinanceTracker.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Service.Implementations
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository) =>
            (_expenseRepository) = (expenseRepository);

        public async Task<BaseResponse<ExpenseModel>> CreateExpenseModelAsync(TransactionViewModel model, Guid userId)
        {
            try
            {
                var newExpenseModel = new ExpenseModel(userId, model.TypeId, model.FinancialAccountId, model.Description, model.Amount);

                await _expenseRepository.CreateAsync(newExpenseModel);

                return new BaseResponse<ExpenseModel>(true, newExpenseModel);
            }
            catch (Exception ex)
            {
                return new BaseResponse<ExpenseModel>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<ExpenseModel>> DeleteExpenseModelAsync(Guid expenseModelId)
        {
            try
            {
                var expenseModel = await _expenseRepository.GetByIdAsync(expenseModelId);

                if (expenseModel == null)
                {
                    return new BaseResponse<ExpenseModel>(false, "No income");
                }

                await _expenseRepository.DeleteAsync(expenseModel);

                return new BaseResponse<ExpenseModel>(true, expenseModel);
            }
            catch (Exception ex)
            {
                return new BaseResponse<ExpenseModel>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<PaginatedList<ExpenseModel>>> GetExpenseModelByFinancialAccountIdAsync(Guid financialAccountId, int pageNow, int pageSize)
        {
            try
            {
                var allExpenseModels = await _expenseRepository.GetByFinancialAccountIdAsync(financialAccountId);

                var count = allExpenseModels.Count();
                if (count == 0)
                {
                    return new BaseResponse<PaginatedList<ExpenseModel>>(true, new PaginatedList<ExpenseModel>(allExpenseModels.ToList(), 1, 1));
                }

                int totalPages = (int)Math.Ceiling(count / (double)pageSize);

                if (pageNow > totalPages)
                {
                    return new BaseResponse<PaginatedList<ExpenseModel>>(false, "No such page");
                }

                var expenseModels = allExpenseModels
                                    .Skip((pageNow - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

                return new BaseResponse<PaginatedList<ExpenseModel>>(true, new PaginatedList<ExpenseModel>(expenseModels, pageNow, totalPages));
            }
            catch (Exception ex)
            {
                return new BaseResponse<PaginatedList<ExpenseModel>>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<PaginatedList<ExpenseModel>>> GetExpenseModelByTypeIdAsync(Guid typeId, int pageNow, int pageSize)
        {
            try
            {
                var allExpenseModels = await _expenseRepository.GetByExpenseTypeIdAsync(typeId);

                var count = allExpenseModels.Count();
                if (count == 0)
                {
                    return new BaseResponse<PaginatedList<ExpenseModel>>(true, new PaginatedList<ExpenseModel>(allExpenseModels.ToList(), 1, 1));
                }

                int totalPages = (int)Math.Ceiling(count / (double)pageSize);

                if (pageNow > totalPages)
                {
                    return new BaseResponse<PaginatedList<ExpenseModel>>(false, "No such page");
                }

                var expenseModels = allExpenseModels
                                    .Skip((pageNow - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

                return new BaseResponse<PaginatedList<ExpenseModel>>(true, new PaginatedList<ExpenseModel>(expenseModels, pageNow, totalPages));
            }
            catch (Exception ex)
            {
                return new BaseResponse<PaginatedList<ExpenseModel>>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<PaginatedList<ExpenseModel>>> GetExpenseModelByUserIdAsync(Guid userId, int pageNow, int pageSize)
        {
            try
            {
                var allExpenseModels = await _expenseRepository.GetByUserIdAsync(userId);

                var count = allExpenseModels.Count();
                if (count == 0)
                {
                    return new BaseResponse<PaginatedList<ExpenseModel>>(true, new PaginatedList<ExpenseModel>(allExpenseModels.ToList(), 1, 1));
                }

                int totalPages = (int)Math.Ceiling(count / (double)pageSize);

                if (pageNow > totalPages)
                {
                    return new BaseResponse<PaginatedList<ExpenseModel>>(false, "No such page");
                }

                var expenseModels = allExpenseModels
                                    .Skip((pageNow - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

                return new BaseResponse<PaginatedList<ExpenseModel>>(true, new PaginatedList<ExpenseModel>(expenseModels, pageNow, totalPages));
            }
            catch (Exception ex)
            {
                return new BaseResponse<PaginatedList<ExpenseModel>>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<IEnumerable<ExpenseModel>>> GetExpenseModelHistoryAsync(Guid typeId, int? month, int? year)
        {
            try
            {
                var expenseModels = await _expenseRepository.GetByExpenseTypeIdAsync(typeId);

                var filteredExpenseModels = expenseModels
                    .Where(em => (month == null || em.CreationData.Month == month.Value) && (year == null || em.CreationData.Year == year.Value));

                return new BaseResponse<IEnumerable<ExpenseModel>>(true, filteredExpenseModels);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<ExpenseModel>>(false, ex.Message);
            }
        }
    }
}
