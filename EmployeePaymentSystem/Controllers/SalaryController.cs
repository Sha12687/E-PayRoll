using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeDAL;
using EmployeePaymentSystem.Models;

namespace EmployeePaymentSystem.Controllers
{
    public class SalaryController : Controller
    {
        private ApplicationDbContext context;

        public SalaryController()
        {
            context = new ApplicationDbContext();
        }
        // GET: Salary

        public ActionResult EmployeeSalary()
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

        public ActionResult Add(int employeeId)
        {
            var employee = context.Employees.FirstOrDefault(e => e.Id == employeeId);
            var  SalaryModel = new SalaryModel
            {

                EmployeeId = employee.Id,
                EmpName = employee.FirstName
            };
            return View(SalaryModel);
        }

        public ActionResult ViewSalary(int employeeId)
        {
            var salary = context.Salaries
                        .Include(s => s.Employee)
                        .SingleOrDefault(s => s.EmployeeId == employeeId);

            if (salary != null)
            {
                SalaryModel convertToViewSchedule = new SalaryModel
                {
                    SalaryId = salary.SalaryId,
                    Amount = salary.Amount,
                    EmployeeId = salary.EmployeeId,
                    EmpName = salary.Employee?.FirstName,  
                };

                return View(convertToViewSchedule);
            }
            else
            {
                
                var noSalaryModel = new SalaryModel
                {
                    EmpName = "No Salary",
                    Amount= 0,
                };

                return View(noSalaryModel);
            }
        }


        [HttpPost]
        public ActionResult Add(SalaryModel salaryModel)
        {
            if (ModelState.IsValid)
            {
                Salary salary=new Salary
                {
                    EmployeeId=salaryModel.EmployeeId,
                    Amount = salaryModel.Amount,
                };
                context.Salaries.Add(salary);
                context.SaveChanges();

                return RedirectToAction("EmployeeSalary", "Salary");
            }
            return View(salaryModel);
        }


        // GET: Salary/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary salary = context.Salaries.Find(id);
            if (salary == null)
            {
                return HttpNotFound();
            }
            return View(salary);
        }

      
        // GET: Salary/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary salary = context.Salaries.Find(id);
            if (salary == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(context.Employees, "Id", "FirstName", salary.EmployeeId);
            return View(salary);
        }

        // POST: Salary/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalaryId,Amount,EmployeeId")] Salary salary)
        {
            if (ModelState.IsValid)
            {
                context.Entry(salary).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(context.Employees, "Id", "FirstName", salary.EmployeeId);
            return View(salary);
        }

        // GET: Salary/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary salary = context.Salaries.Find(id);
            if (salary == null)
            {
                return HttpNotFound();
            }
            return View(salary);
        }

        // POST: Salary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Salary salary = context.Salaries.Find(id);
            context.Salaries.Remove(salary);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
