using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Domain.Models
{
    public class ExpenseModel : BaseModel
    {
        public ExpenseModel() =>
            (UserId, ExpenseTypeId, FinancialAccountId, Amount, Description, CreationData) = (Guid.Empty, Guid.Empty, Guid.Empty, 0, "None", DateTime.Now);
        public ExpenseModel(Guid userId, Guid expenseTypeId, Guid financialAccountId, decimal amount, string description) =>
            (UserId, ExpenseTypeId, FinancialAccountId, Amount, Description, CreationData) = (userId, expenseTypeId, financialAccountId, amount, description, DateTime.Now);

        public Guid UserId { get; set; }
        public Guid ExpenseTypeId { get; set; }
        public Guid FinancialAccountId { get; set; }

        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime CreationData { get; set; }
    }
}
