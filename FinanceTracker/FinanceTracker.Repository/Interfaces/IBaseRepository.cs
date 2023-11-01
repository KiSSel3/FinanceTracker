using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Repository.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        public Task CreateAsync(T item);
        public Task<T> UpdateAsync(T item);
        public Task DeleteAsync(T item);
        public Task<T> GetByIdAsync(Guid id);
        public Task<IEnumerable<T>> GetAllAsync();
    }
}
