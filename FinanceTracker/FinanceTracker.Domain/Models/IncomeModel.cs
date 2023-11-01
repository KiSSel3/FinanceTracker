using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Domain.Models
{
    public class IncomeModel : BaseModel
    {
        public IncomeModel() =>
            (UserId, IncomeTypeId, FinancialAccountId, Amount, Description, CreationData) = (Guid.Empty, Guid.Empty, Guid.Empty, 0, "None", DateTime.Now);
        public IncomeModel(Guid userId, Guid incomeTypeId, Guid financialAccountId, string description, decimal amount) =>
            (UserId, IncomeTypeId, FinancialAccountId, Amount, Description, CreationData) = (userId, incomeTypeId, financialAccountId, amount, description, DateTime.Now);

        public Guid UserId { get; set; }
        public Guid IncomeTypeId { get; set; }
        public Guid FinancialAccountId { get; set; }

        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime CreationData { get; set; }
    }
}
