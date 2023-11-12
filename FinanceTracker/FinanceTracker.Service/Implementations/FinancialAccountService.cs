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
    public class FinancialAccountService : IFinancialAccountService
    {
        private readonly IFinancialAccountRepository _financialAccountRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IIncomeRepository _incomeRepository;

        public FinancialAccountService(IFinancialAccountRepository financialAccountRepository, IExpenseRepository expenseRepository, IIncomeRepository incomeRepository) =>
            (_financialAccountRepository, _expenseRepository, _incomeRepository) = (financialAccountRepository, expenseRepository, incomeRepository);

        public async Task<BaseResponse<FinancialAccountModel>> CreateFinancialAccountAsync(TypeViewModel model, Guid userId)
        {
            try
            {
                var financialAccounts = await _financialAccountRepository.GetByUserIdAsync(userId);

                var financialAccountByName = financialAccounts.FirstOrDefault(item => item.Name.Equals(model.Name));
                if(financialAccountByName != null)
                {
                    return new BaseResponse<FinancialAccountModel>(false, "A financial account by that name already exists.");
                }

                var newFinancialAccount = new FinancialAccountModel(userId, model.Name);

                await _financialAccountRepository.CreateAsync(newFinancialAccount);

                return new BaseResponse<FinancialAccountModel>(true, newFinancialAccount);
            }
            catch (Exception ex)
            {
                return new BaseResponse<FinancialAccountModel>(false, ex.Message);
            }
        }
        public async Task<BaseResponse<FinancialAccountModel>> UpdateFinancialAccountAsync(TypeViewModel model, Guid financialAccountId, Guid userId)
        {
            try
            {
                var financialAccounts = await _financialAccountRepository.GetByUserIdAsync(userId);

                var financialAccountByName = financialAccounts.FirstOrDefault(item => item.Name.Equals(model.Name));
                if (financialAccountByName != null)
                {
                    return new BaseResponse<FinancialAccountModel>(false, "A financial account by that name already exists.");
                }

                var financialAccount = await _financialAccountRepository.GetByIdAsync(financialAccountId);
                if (financialAccount == null)
                {
                    return new BaseResponse<FinancialAccountModel>(false, "No financial account");
                }

                financialAccount.Name = model.Name;

                await _financialAccountRepository.UpdateAsync(financialAccount);

                return new BaseResponse<FinancialAccountModel>(true, financialAccount);
            }
            catch (Exception ex)
            {
                return new BaseResponse<FinancialAccountModel>(false, ex.Message);
            }
        }
        public async Task<BaseResponse<FinancialAccountModel>> DeleteFinancialAccountAsync(Guid financialAccountId)
        {
            try
            {
                var financialAccount = await _financialAccountRepository.GetByIdAsync(financialAccountId);
                if(financialAccount == null)
                {
                    return new BaseResponse<FinancialAccountModel>(false, "No financial account");
                }

                await _financialAccountRepository.DeleteAsync(financialAccount);

                return new BaseResponse<FinancialAccountModel>(true, financialAccount);
            }
            catch (Exception ex)
            {
                return new BaseResponse<FinancialAccountModel>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<IEnumerable<FinancialAccountModel>>> GetFinancialAccountsByUserIdAsync(Guid id)
        {
            try
            {
                var financialAccounts = (await _financialAccountRepository.GetByUserIdAsync(id)).ToList();

                for(int i = 0; i < financialAccounts.Count; ++i)
                {
                    var currentFinancialAccount = financialAccounts.ElementAt(i);

                    currentFinancialAccount.Balance = await GetAmountByFinancialAccountIdAsync(currentFinancialAccount.Id);
                }

                return new BaseResponse<IEnumerable<FinancialAccountModel>>(true, financialAccounts);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<FinancialAccountModel>>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<decimal>> GetTotalAmountByUserIdAsync(Guid id)
        {
            decimal amount = 0;

            try
            {
                var financialAccounts = await _financialAccountRepository.GetByUserIdAsync(id);

                foreach(var financialAccount in financialAccounts)
                {
                    var incomes = await _incomeRepository.GetByFinancialAccountIdAsync(financialAccount.Id);
                    amount += GetAmountByIncome(incomes);

                    var expenses = await _expenseRepository.GetByFinancialAccountIdAsync(financialAccount.Id);
                    amount -= GetAmountByExpense(expenses);
                }

                return new BaseResponse<decimal>(true, amount);
            }
            catch (Exception ex)
            {
                return new BaseResponse<decimal>(false, ex.Message);
            }
        }

        private decimal GetAmountByIncome(IEnumerable<IncomeModel> incomes)
        {
            decimal amount = 0;

            foreach(var income in incomes)
            {
                amount += income.Amount;
            }

            return amount;
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

        private async Task<decimal> GetAmountByFinancialAccountIdAsync(Guid id)
        {
            decimal amount = 0;

            var incomes = await _incomeRepository.GetByFinancialAccountIdAsync(id);
            amount += GetAmountByIncome(incomes);

            var expenses = await _expenseRepository.GetByFinancialAccountIdAsync(id);
            amount -= GetAmountByExpense(expenses);

            return amount;
        }
    }
}
