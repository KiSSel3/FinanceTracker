using FinanceTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Repository.Interfaces
{
    public interface IIncomeTypeRepository : IBaseRepository<IncomeTypeModel>
    {
        public Task<IEnumerable<IncomeTypeModel>> GetByUserIdAsync(Guid id);
    }
}
