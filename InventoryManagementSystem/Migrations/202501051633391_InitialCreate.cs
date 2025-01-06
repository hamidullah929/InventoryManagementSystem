namespace InventoryManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignment",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false),
                        DepartmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .ForeignKey("dbo.Employee", t => t.EmployeeID)
                .Index(t => t.EmployeeID);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Budget = c.Decimal(nullable: false, storeType: "money"),
                        StartDate = c.DateTime(nullable: false),
                        EmployeeID = c.Int(),
                    })
                .PrimaryKey(t => t.DepartmentID)
                .ForeignKey("dbo.Employee", t => t.EmployeeID)
                .Index(t => t.EmployeeID);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        HireDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        PurchaseDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Purchase",
                c => new
                    {
                        PurchaseID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        CustomerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PurchaseID)
                .ForeignKey("dbo.Customer", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductID = c.Int(nullable: false),
                        Title = c.String(maxLength: 50),
                        InventoryNumber = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Departments_DepartmentID = c.Int(),
                        Employees_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Department", t => t.Departments_DepartmentID)
                .ForeignKey("dbo.Employee", t => t.Employees_ID)
                .Index(t => t.Departments_DepartmentID)
                .Index(t => t.Employees_ID);
            
            CreateTable(
                "dbo.DepartmentAssignment",
                c => new
                    {
                        Department_DepartmentID = c.Int(nullable: false),
                        Assignment_EmployeeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Department_DepartmentID, t.Assignment_EmployeeID })
                .ForeignKey("dbo.Department", t => t.Department_DepartmentID, cascadeDelete: true)
                .ForeignKey("dbo.Assignment", t => t.Assignment_EmployeeID, cascadeDelete: true)
                .Index(t => t.Department_DepartmentID)
                .Index(t => t.Assignment_EmployeeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Purchase", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Product", "Employees_ID", "dbo.Employee");
            DropForeignKey("dbo.Product", "Departments_DepartmentID", "dbo.Department");
            DropForeignKey("dbo.Purchase", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.Assignment", "EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.Department", "EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.DepartmentAssignment", "Assignment_EmployeeID", "dbo.Assignment");
            DropForeignKey("dbo.DepartmentAssignment", "Department_DepartmentID", "dbo.Department");
            DropIndex("dbo.DepartmentAssignment", new[] { "Assignment_EmployeeID" });
            DropIndex("dbo.DepartmentAssignment", new[] { "Department_DepartmentID" });
            DropIndex("dbo.Product", new[] { "Employees_ID" });
            DropIndex("dbo.Product", new[] { "Departments_DepartmentID" });
            DropIndex("dbo.Purchase", new[] { "CustomerID" });
            DropIndex("dbo.Purchase", new[] { "ProductID" });
            DropIndex("dbo.Department", new[] { "EmployeeID" });
            DropIndex("dbo.Assignment", new[] { "EmployeeID" });
            DropTable("dbo.DepartmentAssignment");
            DropTable("dbo.Product");
            DropTable("dbo.Purchase");
            DropTable("dbo.Customer");
            DropTable("dbo.Employee");
            DropTable("dbo.Department");
            DropTable("dbo.Assignment");
        }
    }
}
