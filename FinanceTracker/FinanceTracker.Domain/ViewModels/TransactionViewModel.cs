using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Domain.ViewModels
{
    public class TransactionViewModel
    {
        [Required(ErrorMessage = "Выберите тип")]
        public Guid TypeId { get; set; }

        [Required(ErrorMessage = "Выберите финансовый счет")]
        public Guid FinancialAccountId { get; set; }

        [Required(ErrorMessage = "Введите сумму")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Сумма должна быть больше 0.01 $")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Введите описание")]
        public string Description { get; set; }
    }
}
