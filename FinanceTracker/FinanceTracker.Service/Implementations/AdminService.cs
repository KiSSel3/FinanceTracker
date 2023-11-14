using FinanceTracker.Domain.Models;
using FinanceTracker.Domain.Response;
using FinanceTracker.Repository.Interfaces;
using FinanceTracker.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Service.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepository;
        private readonly IIncomeRepository _incomeRepository;
        private readonly IExpenseRepository _expenseRepository;

        public AdminService(IUserRepository userRepository, IIncomeRepository incomeRepository, IExpenseRepository expenseRepository) =>
            (_userRepository, _incomeRepository, _expenseRepository) = (userRepository, incomeRepository, expenseRepository);

        public async Task<BaseResponse<UserModel>> CreateNewAdmin(string login)
        {
            try
            {
                UserModel user = await _userRepository.GetByLoginAsync(login);

                if (user == null)
                {
                    return new BaseResponse<UserModel>(false, "Login entered incorrectly");
                }

                user.Role = Domain.Enums.RoleType.ADMIN;

                await _userRepository.UpdateAsync(user);

                return new BaseResponse<UserModel>(true, user);
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserModel>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<int>> GetAverageAge()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                if (users == null || !users.Any())
                {
                    return new BaseResponse<int>(false, "No users available");
                }

                var averageAge = (int)users.Average(user => user.Age);
                return new BaseResponse<int>(true, averageAge);
            }
            catch (Exception ex)
            {
                return new BaseResponse<int>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<decimal>> GetAverageExpense()
        {
            try
            {
                var expenses = await _expenseRepository.GetAllAsync();
                if (expenses == null)
                {
                    return new BaseResponse<decimal>(false, "No expenses available");
                }

                var averageExpense = expenses.Average(expense => expense.Amount);
                return new BaseResponse<decimal>(true, averageExpense);
            }
            catch (Exception ex)
            {
                return new BaseResponse<decimal>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<decimal>> GetAverageIncome()
        {
            try
            {
                var incomes = await _incomeRepository.GetAllAsync();
                if (incomes == null)
                {
                    return new BaseResponse<decimal>(false, "No incomes available");
                }

                var averageIncomes = incomes.Average(income => income.Amount);
                return new BaseResponse<decimal>(true, averageIncomes);
            }
            catch (Exception ex)
            {
                return new BaseResponse<decimal>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<int>> GetMaxAge()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();

                if (users == null || !users.Any())
                {
                    return new BaseResponse<int>(false, "No users available");
                }

                var maxAge = users.Max(user => user.Age);
                return new BaseResponse<int>(true, maxAge);
            }
            catch (Exception ex)
            {
                return new BaseResponse<int>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<decimal>> GetMaxExpense()
        {
            try
            {
                var expenses = await _expenseRepository.GetAllAsync();

                if (expenses == null)
                {
                    return new BaseResponse<decimal>(false, "No expense available");
                }

                var maxAmount = expenses.Max(item => item.Amount);
                return new BaseResponse<decimal>(true, maxAmount);
            }
            catch (Exception ex)
            {
                return new BaseResponse<decimal>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<decimal>> GetMaxIncome()
        {
            try
            {
                var incomes = await _incomeRepository.GetAllAsync();

                if (incomes == null)
                {
                    return new BaseResponse<decimal>(false, "No expense available");
                }

                var maxAmount = incomes.Max(item => item.Amount);
                return new BaseResponse<decimal>(true, maxAmount);
            }
            catch (Exception ex)
            {
                return new BaseResponse<decimal>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<int>> GetMinAge()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                if (users == null || !users.Any())
                {
                    return new BaseResponse<int>(false, "No users available");
                }

                var minAge = users.Min(user => user.Age);
                return new BaseResponse<int>(true, minAge);
            }
            catch (Exception ex)
            {
                return new BaseResponse<int>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<decimal>> GetMinExpense()
        {
            try
            {
                var expenses = await _expenseRepository.GetAllAsync();

                if (expenses == null)
                {
                    return new BaseResponse<decimal>(false, "No expense available");
                }

                var minAmount = expenses.Min(item => item.Amount);
                return new BaseResponse<decimal>(true, minAmount);
            }
            catch (Exception ex)
            {
                return new BaseResponse<decimal>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<decimal>> GetMinIncome()
        {
            try
            {
                var incomes = await _incomeRepository.GetAllAsync();

                if (incomes == null)
                {
                    return new BaseResponse<decimal>(false, "No expense available");
                }

                var minAmount = incomes.Min(item => item.Amount);
                return new BaseResponse<decimal>(true, minAmount);
            }
            catch (Exception ex)
            {
                return new BaseResponse<decimal>(false, ex.Message);
            }
        }
    }
}