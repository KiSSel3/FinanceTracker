using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Domain.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Необходимо заполнить поле для логина")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить поле для пароля")]
        [MinLength(5, ErrorMessage = "Слишком короткий пароль: минимальная длина 5 букв")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить поле для возраста")]
        [Range(0, 120, ErrorMessage = "Возраст должен быть в диапазоне от 5 до 100 лет")]
        public int Age { get; set; }
    }
}
