using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Domain.Models
{
    public class ExpenseTypeModel : BaseModel
    {
        public ExpenseTypeModel() =>
            (UserId, Name) = (Guid.Empty, "None");
        public ExpenseTypeModel(Guid userId, string name) =>
            (UserId, Name) = (userId, name);

        public Guid UserId { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
    }
}
