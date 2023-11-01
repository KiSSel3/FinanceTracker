using FinanceTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Repository.Interfaces
{
    public interface IIncomeRepository : IBaseRepository<IncomeModel>
    {
        public Task<IEnumerable<IncomeModel>> GetByUserIdAsync(Guid Id);
        public Task<IEnumerable<IncomeModel>> GetByIncomeTypeIdAsync(Guid Id);
        public Task<IEnumerable<IncomeModel>> GetByFinancialAccountIddAsync(Guid Id);
    }
}
