using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Domain.ViewModels
{
    public class TypeViewModel
    {
        [Required(ErrorMessage = "Необходимо заполнить поле для имени")]
        public string Name { get; set; }
    }
}
