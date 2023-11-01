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
    public class ExpenseTypeRepository : IExpenseTypeRepository
    {
        private readonly AppDbContext _db;
        public ExpenseTypeRepository(AppDbContext db) => (_db) = (db);

        public async Task CreateAsync(ExpenseTypeModel item)
        {
            await _db.ExpenseTypes.AddAsync(item);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(ExpenseTypeModel item)
        {
            _db.ExpenseTypes.Remove(item);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<ExpenseTypeModel>> GetAllAsync()
        {
            return await _db.ExpenseTypes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ExpenseTypeModel> GetByIdAsync(Guid id)
        {
            return _db.ExpenseTypes
                .AsNoTracking()
                .FirstOrDefault(type => type.Id.Equals(id));
        }

        public async Task<IEnumerable<ExpenseTypeModel>> GetByUserIdAsync(Guid id)
        {
            return _db.ExpenseTypes
                .AsNoTracking()
                .Where(type => type.UserId.Equals(id));
        }

        public async Task UpdateAsync(ExpenseTypeModel item)
        {
            _db.ExpenseTypes.Update(item);
            await _db.SaveChangesAsync();
        }
    }
}
