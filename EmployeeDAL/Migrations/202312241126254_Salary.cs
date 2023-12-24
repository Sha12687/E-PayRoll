namespace EmployeeDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Salary : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Salaries",
                c => new
                    {
                        SalaryId = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SalaryId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Salaries", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Salaries", new[] { "EmployeeId" });
            DropTable("dbo.Salaries");
        }
    }
}
