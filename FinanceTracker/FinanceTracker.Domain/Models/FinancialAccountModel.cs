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
            (UserId, Name, Balance) = (Guid.Empty, "None", 0);
        public FinancialAccountModel(Guid userId, string name) =>
            (UserId, Name, Balance) = (userId, name, 0);

        public Guid UserId { get; set; }
        public string Name { get; set; } 
        public decimal Balance { get; set; }
    }
}
