using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeePaymentSystem.Models
{
    public class ScheduleModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Schedule date is required.")]
        [Display(Name = "Schedule Date")]
      
        public string ScheduleDate { get; set; }

        [Required(ErrorMessage = "Start time is required.")]
        [Display(Name = "Start Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "End time is required.")]
        [Display(Name = "End Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [MaxLength(255)]
        public string Location { get; set; }
        public int? EmployeeId { get; set; }
       public string EmpName { get; set; }    
    }

}