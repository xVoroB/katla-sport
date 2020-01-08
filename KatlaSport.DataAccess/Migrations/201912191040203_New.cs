namespace KatlaSport.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class New : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.catalogue_products",
                c => new
                    {
                        product_id = c.Int(nullable: false, identity: true),
                        product_name = c.String(nullable: false, maxLength: 60),
                        product_code = c.String(nullable: false, maxLength: 5),
                        product_category_id = c.Int(nullable: false),
                        deleted = c.Boolean(nullable: false),
                        created_by_id = c.Int(nullable: false),
                        created_utc = c.DateTime(nullable: false),
                        updated_by_id = c.Int(nullable: false),
                        updated_utc = c.DateTime(nullable: false),
                        product_description = c.String(maxLength: 300),
                        product_manufacturer_code = c.String(maxLength: 10),
                        product_price = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.product_id)
                .ForeignKey("dbo.product_categories", t => t.product_category_id, cascadeDelete: true)
                .Index(t => t.product_code)
                .Index(t => t.product_category_id);

            CreateTable(
                "dbo.product_categories",
                c => new
                    {
                        category_id = c.Int(nullable: false, identity: true),
                        category_name = c.String(nullable: false, maxLength: 60),
                        category_code = c.String(nullable: false, maxLength: 5),
                        deleted = c.Boolean(nullable: false),
                        created_by_id = c.Int(nullable: false),
                        created_utc = c.DateTime(nullable: false),
                        updated_by_id = c.Int(nullable: false),
                        category_description = c.String(maxLength: 300),
                        updated_utc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.category_id)
                .Index(t => t.category_code);

            CreateTable(
                "dbo.product_hive_section_categories",
                c => new
                    {
                        product_category_id = c.Int(nullable: false),
                        product_hive_section_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.product_category_id, t.product_hive_section_id })
                .ForeignKey("dbo.product_categories", t => t.product_category_id, cascadeDelete: true)
                .ForeignKey("dbo.product_hive_sections", t => t.product_hive_section_id, cascadeDelete: true)
                .Index(t => t.product_category_id)
                .Index(t => t.product_hive_section_id);

            CreateTable(
                "dbo.product_hive_sections",
                c => new
                    {
                        product_hive_section_id = c.Int(nullable: false, identity: true),
                        product_hive_section_name = c.String(nullable: false, maxLength: 60),
                        product_hive_code = c.String(nullable: false, maxLength: 5),
                        deleted = c.Boolean(nullable: false),
                        created_by_id = c.Int(nullable: false),
                        created_utc = c.DateTime(nullable: false),
                        updated_by_id = c.Int(nullable: false),
                        updated_utc = c.DateTime(nullable: false),
                        product_hive_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.product_hive_section_id)
                .ForeignKey("dbo.product_hives", t => t.product_hive_id, cascadeDelete: true)
                .Index(t => t.product_hive_id);

            CreateTable(
                "dbo.product_store_items",
                c => new
                    {
                        product_store_item_id = c.Int(nullable: false, identity: true),
                        product_store_item_product_id = c.Int(nullable: false),
                        product_store_item_hive_section_id = c.Int(nullable: false),
                        product_store_item_quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.product_store_item_id)
                .ForeignKey("dbo.product_hive_sections", t => t.product_store_item_hive_section_id, cascadeDelete: true)
                .ForeignKey("dbo.catalogue_products", t => t.product_store_item_product_id, cascadeDelete: true)
                .Index(t => t.product_store_item_product_id)
                .Index(t => t.product_store_item_hive_section_id);

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
                "dbo.customer_records",
                c => new
                    {
                        customer_id = c.Int(nullable: false, identity: true),
                        customer_name = c.String(),
                        customer_address = c.String(),
                        customer_phone = c.String(),
                    })
                .PrimaryKey(t => t.customer_id);

            CreateTable(
                "dbo.employee",
                c => new
                    {
                        employee_id = c.Int(nullable: false, identity: true),
                        employee_name = c.String(nullable: false, maxLength: 20),
                        employee_location = c.String(nullable: false),
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

            CreateTable(
                "dbo.product_hives",
                c => new
                    {
                        product_hive_id = c.Int(nullable: false, identity: true),
                        product_hive_name = c.String(nullable: false, maxLength: 60),
                        product_hive_code = c.String(nullable: false, maxLength: 5),
                        product_hive_address = c.String(nullable: false, maxLength: 300),
                        deleted = c.Boolean(nullable: false),
                        created_by_id = c.Int(nullable: false),
                        created_utc = c.DateTime(nullable: false),
                        updated_by_id = c.Int(nullable: false),
                        updated_utc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.product_hive_id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.catalogue_products", "product_category_id", "dbo.product_categories");
            DropForeignKey("dbo.product_hive_section_categories", "product_hive_section_id", "dbo.product_hive_sections");
            DropForeignKey("dbo.product_hive_sections", "product_hive_id", "dbo.product_hives");
            DropForeignKey("dbo.product_store_items", "product_store_item_product_id", "dbo.catalogue_products");
            DropForeignKey("dbo.order_products", "stored_item_id", "dbo.product_store_items");
            DropForeignKey("dbo.order_products", "order_id", "dbo.orders");
            DropForeignKey("dbo.orders", "employee_id", "dbo.employee");
            DropForeignKey("dbo.employee", "employee_position_id", "dbo.position");
            DropForeignKey("dbo.employee", "employee_cv_id", "dbo.employee_cv");
            DropForeignKey("dbo.employee", "chief_employee_id", "dbo.employee");
            DropForeignKey("dbo.orders", "customer_id", "dbo.customer_records");
            DropForeignKey("dbo.product_store_items", "product_store_item_hive_section_id", "dbo.product_hive_sections");
            DropForeignKey("dbo.product_hive_section_categories", "product_category_id", "dbo.product_categories");
            DropIndex("dbo.employee", new[] { "employee_cv_id" });
            DropIndex("dbo.employee", new[] { "chief_employee_id" });
            DropIndex("dbo.employee", new[] { "employee_position_id" });
            DropIndex("dbo.orders", new[] { "employee_id" });
            DropIndex("dbo.orders", new[] { "customer_id" });
            DropIndex("dbo.order_products", new[] { "stored_item_id" });
            DropIndex("dbo.order_products", new[] { "order_id" });
            DropIndex("dbo.product_store_items", new[] { "product_store_item_hive_section_id" });
            DropIndex("dbo.product_store_items", new[] { "product_store_item_product_id" });
            DropIndex("dbo.product_hive_sections", new[] { "product_hive_id" });
            DropIndex("dbo.product_hive_section_categories", new[] { "product_hive_section_id" });
            DropIndex("dbo.product_hive_section_categories", new[] { "product_category_id" });
            DropIndex("dbo.product_categories", new[] { "category_code" });
            DropIndex("dbo.catalogue_products", new[] { "product_category_id" });
            DropIndex("dbo.catalogue_products", new[] { "product_code" });
            DropTable("dbo.product_hives");
            DropTable("dbo.position");
            DropTable("dbo.employee_cv");
            DropTable("dbo.employee");
            DropTable("dbo.customer_records");
            DropTable("dbo.orders");
            DropTable("dbo.order_products");
            DropTable("dbo.product_store_items");
            DropTable("dbo.product_hive_sections");
            DropTable("dbo.product_hive_section_categories");
            DropTable("dbo.product_categories");
            DropTable("dbo.catalogue_products");
        }
    }
}
