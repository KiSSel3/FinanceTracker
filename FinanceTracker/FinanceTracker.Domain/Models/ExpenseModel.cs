using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Domain.Models
{
    public class ExpensesModel : BaseModel
    {
        public ExpensesModel() =>
            (ExpensesTypeId, CreationData, Amount) = (Guid.Empty, DateOnly.MinValue, 0);
        public ExpensesModel(Guid expensesTypeId, decimal amount, DateOnly creationData) =>
            (ExpensesTypeId, CreationData, Amount) = (expensesTypeId, creationData, amount);
        public Guid ExpensesTypeId { get; set; }
        public DateOnly CreationData { get; set; }
        public decimal Amount { get; set; }
    }
}
