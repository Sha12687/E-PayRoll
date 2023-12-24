using EmployeeDAL;
using EmployeeDAL.Migrations;
using EmployeePaymentSystem.Models;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeePaymentSystem.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext context=new ApplicationDbContext();
        // GET: Admin
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            var empLsit = context.Employees;
            var convertToViewEmployee =  empLsit.Select(employee=> new UserView
            {
                Id = employee.Id,
                Email = employee.Email,
                UserName = employee.UserName,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                PhoneNumber = employee.PhoneNumber,
              
            });
            return View(convertToViewEmployee);
        }

        public ActionResult ScheduleEmployee()
        {
            var empLsit = context.Employees;
            var convertToViewEmployee = empLsit.Select(employee => new UserView
            {
                Id = employee.Id,
                Email = employee.Email,
                UserName = employee.UserName,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                PhoneNumber = employee.PhoneNumber,
                
            });
            return View(convertToViewEmployee);
        }

        public ActionResult AddSchedule(int employeeId)
        {
          ScheduleModel schedule = new ScheduleModel();
            var employee = context.Employees.FirstOrDefault(e => e.Id == employeeId);
            schedule.EmployeeId = employeeId;
            schedule.EmpName = employee.FirstName ;
            return View(schedule);
        }

        [HttpPost]
        public ActionResult AddSchedule(ScheduleModel scheduleModel)
        {
            string format = "dd/MM/yyyy";
            var shcedule = new Schedule
            {

            ScheduleDate = DateTime.ParseExact(scheduleModel.ScheduleDate, format, CultureInfo.InvariantCulture),
               StartTime = scheduleModel.StartTime.TimeOfDay,
               EndTime= scheduleModel.EndTime.TimeOfDay,
               Location = scheduleModel.Location,
               EmployeeId= scheduleModel.EmployeeId,
               
            };
            if(shcedule != null) {
            context.Schedules.Add(shcedule);
                context.SaveChanges();
                return RedirectToAction("ScheduleEmployee", "Admin");
            }
           return View(scheduleModel);
        }

        public ActionResult ViewSchedule(int employeeId)
        {
            var employeeSchedule = context.Schedules
           .Where(s => s.EmployeeId == employeeId)
           .ToList();
            var convertToViewSchedule = employeeSchedule.Select(sche => new ScheduleModel
            {
                Id = sche.Id,
                ScheduleDate = sche.ScheduleDate.ToString("dd/MM/yyyy"), // Format the date as needed
                StartTime = DateTime.Today.Add(sche.StartTime), // Combine with a default date
                EndTime = DateTime.Today.Add(sche.EndTime), // Combine with a default date
                Location = sche.Location,
                EmployeeId = sche.EmployeeId,
               
            });

            return View(convertToViewSchedule.ToList());
        }
        public ActionResult EmployeeAttendance()
        {
            var empLsit = context.Employees;
            var convertToViewEmployee = empLsit.Select(employee => new UserView
            {
                Id = employee.Id,
                Email = employee.Email,
                UserName = employee.UserName,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                PhoneNumber = employee.PhoneNumber,

            });
            return View(convertToViewEmployee);
        }
        public ActionResult AddAttendance(int employeeId)
        {
            var employee = context.Employees.FirstOrDefault(e => e.Id == employeeId);
            var attendenceModel = new AttendanceModel
            {

                EmployeeId = employee.Id,
                EmpName=employee.FirstName
            };
            return View(attendenceModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAttendance(AttendanceModel attendance)
        {
            if (ModelState.IsValid)
            {

                string format = "dd/MM/yyyy";
                DateTime attendanceDate = DateTime.ParseExact(attendance.Date, format, CultureInfo.InvariantCulture);
                bool attendanceExists = context.Attendances
                    .Any(a => a.EmployeeId == attendance.EmployeeId &&
                              a.Date == attendanceDate);

                if (attendanceExists)
                {
                    // Attendance for the given date already exists, show a message
                    ModelState.AddModelError("Date", "Attendance for this date already exists.");
                    return View(attendance);
                }

                Attendance attendanceEntity = new Attendance
                {
                    EmployeeId = attendance.EmployeeId,
                    IsPresent = attendance.IsPresent,
                    Date = DateTime.ParseExact(attendance.Date, format, CultureInfo.InvariantCulture),
                };

                context.Attendances.Add(attendanceEntity);
                context.SaveChanges();

                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return View(attendance);
            }
        }
        //public ActionResult ViewAttendance2(int employeeId)
        //{
        //    var attendancelist =  context.Attendances
        //   .Where(s => s.EmployeeId == employeeId)
        //   .ToList();
        //    var attendanceView = attendancelist.Select(atten => new AttendanceModel
        //    {
        //       EmployeeId=atten.Id, IsPresent = atten.IsPresent,
        //       Date=atten.Date.ToShortDateString(),

        //    });
        //    return View(attendanceView.ToList());
        //}
        public ActionResult ViewAttendance(int employeeId)
        {
            var attendancelist = context.Attendances
                .Where(s => s.EmployeeId == employeeId)
                .ToList();

            var groupedAttendance = attendancelist
                .GroupBy(a => new { Year = a.Date.Year, Month = a.Date.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Attendances = g.ToList()
                })
                .ToList();

            var attendanceView = groupedAttendance.SelectMany(group => group.Attendances.Select(atten => new AttendanceModel2
            {
                EmployeeId = atten.Id,
                IsPresent = atten.IsPresent,
                Date = atten.Date.ToShortDateString(),
                Year = group.Year,
                Month = group.Month
            }));

            return View(attendanceView.ToList());
        }


     
    }
}