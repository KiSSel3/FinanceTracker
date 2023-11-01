using FinanceTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Domain.Models
{
    public class UserModel : BaseModel
    {
        public UserModel() =>
            (Login, Password, Age, Role) = ("None", "None", 0, RoleType.USER);
        public UserModel(string login, string password, int age, RoleType role = RoleType.USER) =>
            (Login, Password, Age, Role) = (login, password, age, role);

        public string Login { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public RoleType Role { get; set; }
    }
}
