using EmployeeDAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeePaymentSystem.Models
{
    public class AttendanceModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Attendance date is required.")]
        public string Date { get; set; }

        [Required(ErrorMessage = "Attendance status is required.")]
        public bool IsPresent { get; set; }

        // Foreign key for Employee
        public int EmployeeId { get; set; }

       public  string EmpName { get; set; }
        // Navigation property for Employee
       
    }
}