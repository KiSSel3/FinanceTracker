using FinanceTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Repository.Interfaces
{
    public interface IExpenseRepository : IBaseRepository<ExpenseModel>
    {
        public Task<IEnumerable<ExpenseModel>> GetByUserIdAsync(Guid id);
        public Task<IEnumerable<ExpenseModel>> GetByExpenseTypeIdAsync(Guid id);
        public Task<IEnumerable<ExpenseModel>> GetByFinancialAccountIddAsync(Guid id);
    }
}
