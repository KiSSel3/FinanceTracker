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
    public class IncomeRepository : IIncomeRepository
    {
        private readonly AppDbContext _db;
        public IncomeRepository(AppDbContext db) => (_db) = (db);

        public async Task CreateAsync(IncomeModel item)
        {
            await _db.Incomes.AddAsync(item);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(IncomeModel item)
        {
            _db.Incomes.Remove(item);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<IncomeModel>> GetAllAsync()
        {
            return await _db.Incomes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<IncomeModel>> GetByFinancialAccountIddAsync(Guid id)
        {
            return _db.Incomes
                .AsNoTracking()
                .Where(income => income.FinancialAccountId.Equals(id));
        }

        public async Task<IncomeModel> GetByIdAsync(Guid id)
        {
            return _db.Incomes
                .AsNoTracking()
                .FirstOrDefault(income => income.Id.Equals(id));
        }

        public async Task<IEnumerable<IncomeModel>> GetByIncomeTypeIdAsync(Guid id)
        {
            return _db.Incomes
                .AsNoTracking()
                .Where(income => income.IncomeTypeId.Equals(id));
        }

        public async Task<IEnumerable<IncomeModel>> GetByUserIdAsync(Guid id)
        {
            return _db.Incomes
                .AsNoTracking()
                .Where(income => income.UserId.Equals(id));
        }

        public async Task UpdateAsync(IncomeModel item)
        {
            _db.Incomes.Update(item);
            await _db.SaveChangesAsync();
        }
    }
}
