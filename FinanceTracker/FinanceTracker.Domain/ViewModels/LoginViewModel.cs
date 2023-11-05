using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Domain.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Необходимо заполнить поле для логина")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить поле для пароля")]
        public string Password { get; set; }
    }
}
