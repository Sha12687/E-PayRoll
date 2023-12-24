using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDAL
{
    public class Salary
    {
        [Key]
        public int SalaryId { get; set; }

        [Required(ErrorMessage = "Salary amount is required.")]
        public decimal Amount { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
