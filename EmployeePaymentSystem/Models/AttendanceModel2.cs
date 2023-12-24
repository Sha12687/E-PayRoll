using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeePaymentSystem.Models
{
    public class AttendanceModel2
    {
        public int EmployeeId { get; set; }
        public bool IsPresent { get; set; }
        public string Date { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }

}