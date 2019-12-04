namespace KatlaSport.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddEmployeesAndOrdersTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.order_products",
                c => new
                    {
                        order_products_id = c.Int(nullable: false, identity: true),
                        order_id = c.Int(nullable: false),
                        stored_item_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.order_products_id)
                .ForeignKey("dbo.orders", t => t.order_id, cascadeDelete: true)
                .ForeignKey("dbo.product_store_items", t => t.stored_item_id, cascadeDelete: true)
                .Index(t => t.order_id)
                .Index(t => t.stored_item_id);

            CreateTable(
                "dbo.orders",
                c => new
                    {
                        order_id = c.Int(nullable: false, identity: true),
                        customer_id = c.Int(nullable: false),
                        employee_id = c.Int(nullable: false),
                        order_acceptance_date = c.DateTime(nullable: false),
                        order_dispatch_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.order_id)
                .ForeignKey("dbo.customer_records", t => t.customer_id, cascadeDelete: true)
                .ForeignKey("dbo.employee", t => t.employee_id, cascadeDelete: true)
                .Index(t => t.customer_id)
                .Index(t => t.employee_id);

            CreateTable(
                "dbo.employee",
                c => new
                    {
                        employee_id = c.Int(nullable: false, identity: true),
                        employee_name = c.String(nullable: false, maxLength: 20),
                        employee_birth_date = c.DateTime(nullable: false),
                        employee_position_id = c.Int(nullable: false),
                        chief_employee_id = c.Int(),
                        employee_cv_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.employee_id)
                .ForeignKey("dbo.employee", t => t.chief_employee_id)
                .ForeignKey("dbo.employee_cv", t => t.employee_cv_id, cascadeDelete: true)
                .ForeignKey("dbo.position", t => t.employee_position_id, cascadeDelete: true)
                .Index(t => t.employee_position_id)
                .Index(t => t.chief_employee_id)
                .Index(t => t.employee_cv_id);

            CreateTable(
                "dbo.employee_cv",
                c => new
                    {
                        employee_cv_id = c.Int(nullable: false, identity: true),
                        file = c.Binary(nullable: false),
                        file_name = c.String(nullable: false),
                        updated_utc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.employee_cv_id);

            CreateTable(
                "dbo.position",
                c => new
                    {
                        position_id = c.Int(nullable: false, identity: true),
                        position_name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.position_id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.order_products", "stored_item_id", "dbo.product_store_items");
            DropForeignKey("dbo.order_products", "order_id", "dbo.orders");
            DropForeignKey("dbo.orders", "employee_id", "dbo.employee");
            DropForeignKey("dbo.employee", "employee_position_id", "dbo.position");
            DropForeignKey("dbo.employee", "employee_cv_id", "dbo.employee_cv");
            DropForeignKey("dbo.employee", "chief_employee_id", "dbo.employee");
            DropForeignKey("dbo.orders", "customer_id", "dbo.customer_records");
            DropIndex("dbo.employee", new[] { "employee_cv_id" });
            DropIndex("dbo.employee", new[] { "chief_employee_id" });
            DropIndex("dbo.employee", new[] { "employee_position_id" });
            DropIndex("dbo.orders", new[] { "employee_id" });
            DropIndex("dbo.orders", new[] { "customer_id" });
            DropIndex("dbo.order_products", new[] { "stored_item_id" });
            DropIndex("dbo.order_products", new[] { "order_id" });
            DropTable("dbo.position");
            DropTable("dbo.employee_cv");
            DropTable("dbo.employee");
            DropTable("dbo.orders");
            DropTable("dbo.order_products");
        }
    }
}
