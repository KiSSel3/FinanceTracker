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
    public class IncomeTypeRepository : IIncomeTypeRepository
    {
        private readonly AppDbContext _db;
        public IncomeTypeRepository(AppDbContext db) => (_db) = (db);

        public async Task CreateAsync(IncomeTypeModel item)
        {
            await _db.IncomeTypes.AddAsync(item);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(IncomeTypeModel item)
        {
            _db.IncomeTypes.Remove(item);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<IncomeTypeModel>> GetAllAsync()
        {
            return await _db.IncomeTypes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IncomeTypeModel> GetByIdAsync(Guid id)
        {
            return _db.IncomeTypes
                .AsNoTracking()
                .FirstOrDefault(type => type.Id.Equals(id));
        }

        public async Task<IEnumerable<IncomeTypeModel>> GetByUserIdAsync(Guid id)
        {
            return _db.IncomeTypes
                .AsNoTracking()
                .Where(type => type.UserId.Equals(id));
        }

        public async Task UpdateAsync(IncomeTypeModel item)
        {
            _db.IncomeTypes.Update(item);
            await _db.SaveChangesAsync();
        }
    }
}
