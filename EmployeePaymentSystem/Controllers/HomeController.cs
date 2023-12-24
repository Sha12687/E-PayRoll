using EmployeeDAL;
using EmployeePaymentSystem.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EmployeePaymentSystem.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public ActionResult Registration()
        {
            return View();
        }

        public ActionResult Login()
        {


            return View();
        }
        public ActionResult EmployeRegistration(UserView user)
        {
            if (context.Admins.Any(a => a.Email == user.Email) || context.Employees.Any(e => e.Email == user.Email))
            {
                // Email already registered
                ModelState.AddModelError("Email", "Email already registered with us.");
                return View("Registration", user);
            }
            else if (context.Admins.Any(a => a.UserName == user.UserName) || context.Employees.Any(e => e.UserName == user.UserName))
            {
                // Username already registered
                ModelState.AddModelError("UserName", "Username already registered with us.");

                return View("Registration", user);
            }
            if (user.UserType == 2)
            {
                Employee employee = new Employee
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    RoleId = user.UserType
                };
                var passwordHash = new PasswordHasher<Employee>();
                employee.Password = passwordHash.HashPassword(employee, user.Password);
                context.Employees.Add(employee);
                context.SaveChanges();

                return RedirectToAction("Index", "Employee");
            }
            else
            {
                Admin newadmin = new Admin
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    RoleId = user.UserType
                };
                var passwordHash = new PasswordHasher<Admin>();
                newadmin.Password = passwordHash.HashPassword(newadmin, user.Password);
                context.Admins.Add(newadmin);
                context.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }
        }

        public ActionResult AdminLogin()
        {


            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(LoginViewModel loginView)
        {
            var isAdmin = VerifyAdminCredentials(loginView.UserName, loginView.Password);

            if (isAdmin)
            {
                FormsAuthentication.SetAuthCookie(loginView.UserName, false);
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                // If authentication fails, you may want to show an error message.
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return View(loginView);
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login","Home");
        }

        private bool AuthenticateAdmin(string username, string password)
        {
           
            var admin = context.Admins.SingleOrDefault(a => a.UserName == username && a.Password == password);

            // Return true if an admin is found, otherwise false.
            return admin != null;
        }
        public bool VerifyAdminCredentials(string username, string password)
        {
            // Your logic to check username and password in the database
            var admin = context.Admins.FirstOrDefault(a => a.UserName == username);

            if (admin != null)
            {
                var passwordHasher = new PasswordHasher<Admin>();
                var result = passwordHasher.VerifyHashedPassword(admin, admin.Password, password);

                if (result == PasswordVerificationResult.Success)
                {
                    return true; // Username and password are correct
                }
            }

            return false; // Invalid username or password
        }

    }
}