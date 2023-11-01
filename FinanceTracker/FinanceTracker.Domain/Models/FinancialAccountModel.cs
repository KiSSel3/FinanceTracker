using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Domain.Models
{
    public class FinancialAccountModel : BaseModel
    {
        public FinancialAccountModel() =>
            (UserId, Name) = (Guid.Empty, "None");
        public FinancialAccountModel(Guid userId, string name) =>
            (UserId, Name) = (userId, name);

        public Guid UserId { get; set; }
        public string Name { get; set; } 
    }
}
