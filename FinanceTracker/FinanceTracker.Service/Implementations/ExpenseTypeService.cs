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
    public class ExpenseTypeService : IExpenseTypeService
    {
        private readonly IExpenseTypeRepository _expenseTypeRepository;
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseTypeService(IExpenseTypeRepository expenseTypeRepository, IExpenseRepository expenseRepository) =>
            (_expenseTypeRepository, _expenseRepository) = (expenseTypeRepository, expenseRepository);

        public async Task<BaseResponse<ExpenseTypeModel>> CreateExpenseTypeAsync(TypeViewModel model, Guid userId)
        {
            try
            {
                var expenseTypes = await _expenseTypeRepository.GetByUserIdAsync(userId);

                var expenseTypeByName = expenseTypes.FirstOrDefault(item => item.Name.Equals(model.Name));
                if (expenseTypeByName != null)
                {
                    return new BaseResponse<ExpenseTypeModel>(false, "A expense type by that name already exists.");
                }

                var newExpenseType = new ExpenseTypeModel(userId, model.Name);

                await _expenseTypeRepository.CreateAsync(newExpenseType);

                return new BaseResponse<ExpenseTypeModel>(true, newExpenseType);
            }
            catch (Exception ex)
            {
                return new BaseResponse<ExpenseTypeModel>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<ExpenseTypeModel>> DeleteExpenseTypeAsync(Guid expenseTypeId)
        {
            try
            {
                var expenseType = await _expenseTypeRepository.GetByIdAsync(expenseTypeId);
                if (expenseType == null)
                {
                    return new BaseResponse<ExpenseTypeModel>(false, "No expense type");
                }

                await _expenseTypeRepository.DeleteAsync(expenseType);

                return new BaseResponse<ExpenseTypeModel>(true, expenseType);
            }
            catch (Exception ex)
            {
                return new BaseResponse<ExpenseTypeModel>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<IEnumerable<ExpenseTypeModel>>> GetExpenseTypesByUserIdAsync(Guid id)
        {
            try
            {
                var expenseTypes = (await _expenseTypeRepository.GetByUserIdAsync(id)).ToList();

                for(int i = 0; i < expenseTypes.Count; ++i)
                {
                    var currentExpenseType = expenseTypes.ElementAt(i);

                    var expenses = await _expenseRepository.GetByExpenseTypeIdAsync(currentExpenseType.Id);

                    currentExpenseType.Balance = GetAmountByExpense(expenses);
                }

                return new BaseResponse<IEnumerable<ExpenseTypeModel>>(true, expenseTypes);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<ExpenseTypeModel>>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<ExpenseTypeModel>> UpdateExpenseTypeAsync(TypeViewModel model, Guid expenseTypeId, Guid userId)
        {
            try
            {
                var expenseTypes = await _expenseTypeRepository.GetByUserIdAsync(userId);

                var expenseTypeByName = expenseTypes.FirstOrDefault(item => item.Name.Equals(model.Name));
                if (expenseTypeByName != null)
                {
                    return new BaseResponse<ExpenseTypeModel>(false, "A expense type by that name already exists.");
                }

                var expenseType = await _expenseTypeRepository.GetByIdAsync(expenseTypeId);
                if (expenseType == null)
                {
                    return new BaseResponse<ExpenseTypeModel>(false, "No expense type");
                }

                expenseType.Name = model.Name;

                await _expenseTypeRepository.UpdateAsync(expenseType);

                return new BaseResponse<ExpenseTypeModel>(true, expenseType);
            }
            catch (Exception ex)
            {
                return new BaseResponse<ExpenseTypeModel>(false, ex.Message);
            }
        }

        private decimal GetAmountByExpense(IEnumerable<ExpenseModel> expenses)
        {
            decimal amount = 0;

            foreach (var expense in expenses)
            {
                amount += expense.Amount;
            }

            return amount;
        }
    }
}
