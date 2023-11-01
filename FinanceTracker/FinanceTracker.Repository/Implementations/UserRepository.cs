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
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        public UserRepository(AppDbContext db) => (_db) = (db);

        public async Task CreateAsync(UserModel item)
        {
            await _db.Users.AddAsync(item);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(UserModel item)
        {
            _db.Users.Remove(item);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            return await _db.Users
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<UserModel> GetByIdAsync(Guid id)
        {
            return _db.Users
                .AsNoTracking()
                .FirstOrDefault(user => user.Id.Equals(id));
        }

        public async Task<UserModel> GetByLoginAsync(string login)
        {
            return _db.Users
                .AsNoTracking()
                .FirstOrDefault(user => user.Login.Equals(login));
        }

        public async Task UpdateAsync(UserModel item)
        {
            _db.Users.Update(item);
            await _db.SaveChangesAsync();
        }
    }
}
