using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDAL
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("EmployeePayRoll")
        {
        }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        

            // Your other configurations...

            base.OnModelCreating(modelBuilder);
        }
    }
}
