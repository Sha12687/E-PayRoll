using EmployeeDAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeePaymentSystem.Models
{
    public class SalaryModel
    {
        public int SalaryId { get; set; }

        [Required(ErrorMessage = "Salary amount is required.")]
        public decimal Amount { get; set; }
        public int EmployeeId { get; set; }
        public string  EmpName { get; set; }
    }
}