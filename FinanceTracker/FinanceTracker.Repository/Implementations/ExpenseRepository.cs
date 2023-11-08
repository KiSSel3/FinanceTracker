using FinanceTracker.Domain.Models;
using FinanceTracker.Repository.Context;
using FinanceTracker.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Repository.Implementations
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly AppDbContext _db;
        public ExpenseRepository(AppDbContext db) => (_db) = (db);

        public async Task CreateAsync(ExpenseModel item)
        {
            await _db.Expenses.AddAsync(item);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(ExpenseModel item)
        {
            _db.Expenses.Remove(item);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<ExpenseModel>> GetAllAsync()
        {
            return await _db.Expenses
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<ExpenseModel>> GetByExpenseTypeIdAsync(Guid id)
        {
            return _db.Expenses
                .AsNoTracking()
                .Where(expense => expense.ExpenseTypeId.Equals(id));
        }

        public async Task<IEnumerable<ExpenseModel>> GetByFinancialAccountIdAsync(Guid id)
        {
            return _db.Expenses
                .AsNoTracking()
                .Where(expense => expense.FinancialAccountId.Equals(id));
        }

        public async Task<ExpenseModel> GetByIdAsync(Guid id)
        {
            return _db.Expenses
                .AsNoTracking()
                .FirstOrDefault(expense => expense.Id.Equals(id));
        }

        public async Task<IEnumerable<ExpenseModel>> GetByUserIdAsync(Guid id)
        {
            return _db.Expenses
                .AsNoTracking()
                .Where(expense => expense.UserId.Equals(id));
        }

        public async Task UpdateAsync(ExpenseModel item)
        {
            _db.Expenses.Update(item);
            await _db.SaveChangesAsync();
        }
    }
}
