namespace EmployeeDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uniqueEmailAndUserId : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admins", "Email", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Employees", "Email", c => c.String(nullable: false, maxLength: 255));
            CreateIndex("dbo.Admins", "Email", unique: true, name: "IX_UniqueAdminEmail");
            CreateIndex("dbo.Admins", "UserName", unique: true, name: "IX_UniqueAdminUserName");
            CreateIndex("dbo.Employees", "Email", unique: true, name: "IX_UniqueEmpUserEmail");
            CreateIndex("dbo.Employees", "UserName", unique: true, name: "IX_UniqueEmpUserName");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Employees", "IX_UniqueEmpUserName");
            DropIndex("dbo.Employees", "IX_UniqueEmpUserEmail");
            DropIndex("dbo.Admins", "IX_UniqueAdminUserName");
            DropIndex("dbo.Admins", "IX_UniqueAdminEmail");
            AlterColumn("dbo.Employees", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Admins", "Email", c => c.String(nullable: false));
        }
    }
}
