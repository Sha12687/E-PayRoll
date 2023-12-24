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
            var convertToViewSchedule = employeeSchedule.Select(employee => new ScheduleModel
            {
                Id = employee.Id,
                ScheduleDate = employee.ScheduleDate.ToString("dd/MM/yyyy"), // Format the date as needed
                StartTime = DateTime.Today.Add(employee.StartTime), // Combine with a default date
                EndTime = DateTime.Today.Add(employee.EndTime), // Combine with a default date
                Location = employee.Location,
                EmployeeId = employee.EmployeeId,
               
            });

            return View(convertToViewSchedule.ToList());
        }

    }
}