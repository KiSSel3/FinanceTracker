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
    public class FinancialAccountRepository : IFinancialAccountRepository
    {
        private readonly AppDbContext _db;
        public FinancialAccountRepository(AppDbContext db) => (_db) = (db);

        public async Task CreateAsync(FinancialAccountModel item)
        {
            await _db.FinancialAccounts.AddAsync(item);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(FinancialAccountModel item)
        {
            _db.FinancialAccounts.Remove(item);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<FinancialAccountModel>> GetAllAsync()
        {
            return await _db.FinancialAccounts
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<FinancialAccountModel> GetByIdAsync(Guid id)
        {
            return _db.FinancialAccounts
                .AsNoTracking()
                .FirstOrDefault(account => account.Id.Equals(id));
        }

        public async Task<IEnumerable<FinancialAccountModel>> GetByUserIdAsync(Guid id)
        {
            return _db.FinancialAccounts
                .AsNoTracking()
                .Where(account => account.UserId.Equals(id));
        }

        public async Task UpdateAsync(FinancialAccountModel item)
        {
            _db.FinancialAccounts.Update(item);
            await _db.SaveChangesAsync();
        }
    }
}
