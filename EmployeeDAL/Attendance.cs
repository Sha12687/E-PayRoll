using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDAL
{
    public class Attendance
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Attendance date is required.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Attendance status is required.")]
        public bool IsPresent { get; set; }

        // Foreign key for Employee
        public int? EmployeeId { get; set; }

        // Navigation property for Employee
        public virtual Employee Employee { get; set; }
    }
}
