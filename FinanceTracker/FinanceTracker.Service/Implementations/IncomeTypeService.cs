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
    public class IncomeTypeService : IIncomeTypeService
    {
        private readonly IIncomeTypeRepository _incomeTypeRepository;
        private readonly IIncomeRepository _incomeRepository;
        public IncomeTypeService(IIncomeTypeRepository incomeTypeRepository, IIncomeRepository incomeRepository) =>
            (_incomeTypeRepository, _incomeRepository) = (incomeTypeRepository, incomeRepository);

        public async Task<BaseResponse<IncomeTypeModel>> CreateIncomeTypeAsync(TypeViewModel model, Guid userId)
        {
            try
            {
                var incomeTypes = await _incomeTypeRepository.GetByUserIdAsync(userId);

                var incomeTypeByName = incomeTypes.FirstOrDefault(item => item.Name.Equals(model.Name));
                if (incomeTypeByName != null)
                {
                    return new BaseResponse<IncomeTypeModel>(false, "A income type by that name already exists.");
                }

                var newIncomeType = new IncomeTypeModel(userId, model.Name);

                await _incomeTypeRepository.CreateAsync(newIncomeType);

                return new BaseResponse<IncomeTypeModel>(true, newIncomeType);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IncomeTypeModel>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<IncomeTypeModel>> DeleteIncomeTypeAsync(Guid incomeTypeModelId)
        {
            try
            {
                var incomeType= await _incomeTypeRepository.GetByIdAsync(incomeTypeModelId);
                if (incomeType == null)
                {
                    return new BaseResponse<IncomeTypeModel>(false, "No income type");
                }

                await _incomeTypeRepository.DeleteAsync(incomeType);

                return new BaseResponse<IncomeTypeModel>(true, incomeType);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IncomeTypeModel>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<IEnumerable<IncomeTypeModel>>> GetIncomeTypesByUserIdAsync(Guid id)
        {
            try
            {
                var incomeTypes = (await _incomeTypeRepository.GetByUserIdAsync(id)).ToList();

                for (int i = 0; i < incomeTypes.Count; ++i)
                {
                    var currentIncomeType = incomeTypes.ElementAt(i);

                    var incomes = await _incomeRepository.GetByIncomeTypeIdAsync(currentIncomeType.Id);

                    currentIncomeType.Balance = GetAmountByIncome(incomes);
                }

                return new BaseResponse<IEnumerable<IncomeTypeModel>>(true, incomeTypes);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<IncomeTypeModel>>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<IncomeTypeModel>> UpdateIncomeTypeAsync(TypeViewModel model, Guid incomeTypeModelId, Guid userId)
        {
            try
            {
                var incomeTypes = await _incomeTypeRepository.GetByUserIdAsync(userId);

                var incomeTypeByName = incomeTypes.FirstOrDefault(item => item.Name.Equals(model.Name));
                if (incomeTypeByName != null)
                {
                    return new BaseResponse<IncomeTypeModel>(false, "A income type by that name already exists.");
                }

                var incomeType = await _incomeTypeRepository.GetByIdAsync(incomeTypeModelId);
                if (incomeType == null)
                {
                    return new BaseResponse<IncomeTypeModel>(false, "No income type");
                }

                incomeType.Name = model.Name;

                await _incomeTypeRepository.UpdateAsync(incomeType);

                return new BaseResponse<IncomeTypeModel>(true, incomeType);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IncomeTypeModel>(false, ex.Message);
            }
        }

        private decimal GetAmountByIncome(IEnumerable<IncomeModel> incomes)
        {
            decimal amount = 0;

            foreach (var income in incomes)
            {
                amount += income.Amount;
            }

            return amount;
        }
    }
}
