using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDAL
{
    public class Schedule
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Schedule date is required.")]
        public DateTime ScheduleDate { get; set; }

        [Required(ErrorMessage = "Start time is required.")]
        public TimeSpan StartTime { get; set; }

        [Required(ErrorMessage = "End time is required.")]
        public TimeSpan EndTime { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [MaxLength(255)]
        public string Location { get; set; }

        // Additional properties as needed

        public int? EmployeeId { get; set; } // Foreign key to Employee
        public virtual Employee Employee { get; set; }

        // You may add more properties as needed
    }

}
