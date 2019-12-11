﻿using System.IO;
using KatlaSport.DataAccess.EmployeeInfo;
using KatlaSport.DataAccess.Orders;

namespace KatlaSport.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using KatlaSport.DataAccess.CustomerCatalogue;
        //using KatlaSport.DataAccess.EmployeeInfo;
    using KatlaSport.DataAccess.ProductCatalogue;
    using KatlaSport.DataAccess.ProductStore;
    using KatlaSport.DataAccess.ProductStoreHive;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        /*
        drop table dbo.product_store_items
        drop table dbo.product_hive_section_categories
        drop table dbo.catalogue_products
        drop table dbo.product_categories
        drop table dbo.product_hive_sections
        drop table dbo.product_hives
        drop table dbo.customer_records
        drop table dbo.__MigrationHistory

        select * from dbo.catalogue_products
        select * from dbo.product_categories

        select * from dbo.product_hive_sections
        select * from dbo.product_hives
        */

        public Configuration()
        {
            ContextKey = "ApplicationData";
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
            SetSqlGenerator("System.Data.SqlClient", new CustomSqlServerMigrationSqlGenerator());
        }

        protected override void Seed(ApplicationDbContext context)
        {
            SeedDatabase(context);
        }

        [Conditional("DEBUG")]
        private void SeedDatabase(ApplicationDbContext context)
        {
            var timestamp = DateTime.UtcNow;
            var creatorId = 1;

            context.ProductCategories.AddOrUpdate(
                i => i.Id,
                new ProductCategory
                {
                    Id = 1,
                    Name = "Running shoes",
                    Code = "RUNSH",
                    IsDeleted = false,
                    CreatedBy = creatorId,
                    LastUpdatedBy = creatorId,
                    LastUpdated = timestamp
                },
                new ProductCategory
                {
                    Id = 2,
                    Name = "Running clothing",
                    Code = "RUNCL",
                    IsDeleted = false,
                    CreatedBy = creatorId,
                    LastUpdatedBy = creatorId,
                    LastUpdated = timestamp
                },
                new ProductCategory
                {
                    Id = 3,
                    Name = "Bicycling",
                    Code = "BICYC",
                    IsDeleted = false,
                    CreatedBy = creatorId,
                    LastUpdatedBy = creatorId,
                    LastUpdated = timestamp
                });

            context.CatalogueProducts.AddOrUpdate(
                i => i.Id,
                new CatalogueProduct
                {
                    Id = 1,
                    Name = "Kyak Men Shoes",
                    Code = "KYME1",
                    CategoryId = 1,
                    IsDeleted = false,
                    CreatedBy = creatorId,
                    LastUpdatedBy = creatorId,
                    LastUpdated = timestamp,
                    ManufacturerCode = "some"
                },
                new CatalogueProduct
                {
                    Id = 2,
                    Name = "Top-top Men Shoes",
                    Code = "TTME1",
                    CategoryId = 1,
                    IsDeleted = false,
                    CreatedBy = creatorId,
                    LastUpdatedBy = creatorId,
                    LastUpdated = timestamp,
                    ManufacturerCode = "body"
                },
                new CatalogueProduct
                {
                    Id = 3,
                    Name = "Abibas T-Shirt",
                    Code = "ABIT1",
                    CategoryId = 2,
                    IsDeleted = false,
                    CreatedBy = creatorId,
                    LastUpdatedBy = creatorId,
                    LastUpdated = timestamp,
                    ManufacturerCode = "once"
                },
                new CatalogueProduct
                {
                    Id = 4,
                    Name = "Pedali 360 Bicycle",
                    Code = "PBYC1",
                    CategoryId = 3,
                    IsDeleted = false,
                    CreatedBy = creatorId,
                    LastUpdatedBy = creatorId,
                    LastUpdated = timestamp,
                    ManufacturerCode = "told me"
                });

            context.StoreHives.AddOrUpdate(
                i => i.Id,
                new StoreHive
                {
                    Id = 1,
                    Name = "Gorka Minsk",
                    Address = "Minsk, Chaveza-30",
                    Code = "HIVE1",
                    IsDeleted = false,
                    CreatedBy = creatorId,
                    LastUpdatedBy = creatorId,
                    LastUpdated = timestamp
                },
                new StoreHive
                {
                    Id = 2,
                    Name = "Shabany Minsk",
                    Address = "Minsk, Zarechnaya-47",
                    Code = "HIVE2",
                    IsDeleted = false,
                    CreatedBy = creatorId,
                    LastUpdatedBy = creatorId,
                    LastUpdated = timestamp
                },
                new StoreHive
                {
                    Id = 3,
                    Name = "Tugolitsa Bobruisk",
                    Address = "Bobruisk, Vanceti-99",
                    Code = "HIVE3",
                    IsDeleted = false,
                    CreatedBy = creatorId,
                    LastUpdatedBy = creatorId,
                    LastUpdated = timestamp
                });

            context.HiveSections.AddOrUpdate(
                i => i.Id,
                new StoreHiveSection
                {
                    Id = 1,
                    Name = "MSQ #1",
                    StoreHiveId = 1,
                    Code = "HSE11",
                    IsDeleted = false,
                    CreatedBy = creatorId,
                    LastUpdatedBy = creatorId,
                    LastUpdated = timestamp
                },
                new StoreHiveSection
                {
                    Id = 2,
                    Name = "MSQ #2",
                    StoreHiveId = 1,
                    Code = "HSE12",
                    IsDeleted = false,
                    CreatedBy = creatorId,
                    LastUpdatedBy = creatorId,
                    LastUpdated = timestamp
                },
                new StoreHiveSection
                {
                    Id = 3,
                    Name = "MSQ #3",
                    StoreHiveId = 2,
                    Code = "HSE21",
                    IsDeleted = false,
                    CreatedBy = creatorId,
                    LastUpdatedBy = creatorId,
                    LastUpdated = timestamp
                },
                new StoreHiveSection
                {
                    Id = 4,
                    Name = "MSQ #4",
                    StoreHiveId = 2,
                    Code = "HSE22",
                    IsDeleted = false,
                    CreatedBy = creatorId,
                    LastUpdatedBy = creatorId,
                    LastUpdated = timestamp
                },
                new StoreHiveSection
                {
                    Id = 5,
                    Name = "BBR #1",
                    StoreHiveId = 3,
                    Code = "HSE31",
                    IsDeleted = false,
                    CreatedBy = creatorId,
                    LastUpdatedBy = creatorId,
                    LastUpdated = timestamp
                },
                new StoreHiveSection
                {
                    Id = 6,
                    Name = "BBR #2",
                    StoreHiveId = 3,
                    Code = "HSE32",
                    IsDeleted = false,
                    CreatedBy = creatorId,
                    LastUpdatedBy = creatorId,
                    LastUpdated = timestamp
                });

            context.SectionCategories.AddOrUpdate(
                i => new { i.ProductCategoryId, i.StoreHiveSectionId },
                new StoreHiveSectionCategory
                {
                    ProductCategoryId = 1,
                    StoreHiveSectionId = 1
                },
                new StoreHiveSectionCategory
                {
                    ProductCategoryId = 1,
                    StoreHiveSectionId = 3
                },
                new StoreHiveSectionCategory
                {
                    ProductCategoryId = 1,
                    StoreHiveSectionId = 5
                },
                new StoreHiveSectionCategory
                {
                    ProductCategoryId = 2,
                    StoreHiveSectionId = 1
                },
                new StoreHiveSectionCategory
                {
                    ProductCategoryId = 2,
                    StoreHiveSectionId = 3
                },
                new StoreHiveSectionCategory
                {
                    ProductCategoryId = 2,
                    StoreHiveSectionId = 5
                },
                new StoreHiveSectionCategory
                {
                    ProductCategoryId = 3,
                    StoreHiveSectionId = 2
                },
                new StoreHiveSectionCategory
                {
                    ProductCategoryId = 3,
                    StoreHiveSectionId = 4
                },
                new StoreHiveSectionCategory
                {
                    ProductCategoryId = 3,
                    StoreHiveSectionId = 6
                });

            context.StoreItems.AddOrUpdate(
                i => i.Id,
                new StoreItem // #1
                {
                    Id = 1,
                    Quantity = 10,
                    HiveSectionId = 1,
                    ProductId = 1
                },
                new StoreItem
                {
                    Id = 2,
                    Quantity = 1,
                    HiveSectionId = 3,
                    ProductId = 1
                },
                new StoreItem
                {
                    Id = 3,
                    Quantity = 0,
                    HiveSectionId = 5,
                    ProductId = 1
                },
                new StoreItem // #2
                {
                    Id = 4,
                    Quantity = 5,
                    HiveSectionId = 1,
                    ProductId = 2
                },
                new StoreItem
                {
                    Id = 5,
                    Quantity = 100,
                    HiveSectionId = 3,
                    ProductId = 2
                },
                new StoreItem
                {
                    Id = 6,
                    Quantity = 10,
                    HiveSectionId = 5,
                    ProductId = 2
                },
                new StoreItem // #3
                {
                    Id = 7,
                    Quantity = 10,
                    HiveSectionId = 1,
                    ProductId = 3
                },
                new StoreItem
                {
                    Id = 8,
                    Quantity = 10,
                    HiveSectionId = 3,
                    ProductId = 3
                },
                new StoreItem
                {
                    Id = 9,
                    Quantity = 10,
                    HiveSectionId = 5,
                    ProductId = 3
                },
                new StoreItem // #4
                {
                    Id = 13,
                    Quantity = 4,
                    HiveSectionId = 2,
                    ProductId = 4
                },
                new StoreItem
                {
                    Id = 14,
                    Quantity = 0,
                    HiveSectionId = 4,
                    ProductId = 4
                },
                new StoreItem
                {
                    Id = 15,
                    Quantity = 0,
                    HiveSectionId = 6,
                    ProductId = 4
                });

            context.Customers.AddOrUpdate(
                i => i.Id,
                new Customer
                {
                    Id = 1,
                    Name = "Oleg Alexandrov",
                    Address = "Minsk, Goretskogo-31",
                    Phone = "+37529-4345834"
                },
                new Customer
                {
                    Id = 2,
                    Name = "Gleb Pavlov",
                    Address = "Minsk, Cechota-21",
                    Phone = "+37529-3282943"
                },
                new Customer
                {
                    Id = 3,
                    Name = "Sergey Tatarinov",
                    Address = "Borisov, 100 let BSSR",
                    Phone = "+37529-9834782"
                },
                new Customer
                {
                    Id = 4,
                    Name = "Alexander Alexandrov",
                    Address = "Brest, Repina-7",
                    Phone = "+37529-9832872"
                });

            context.Positions.AddOrUpdate(
                i => i.Id,
                new Position
                {
                    Id = 1,
                    Name = "Director"
                },
                new Position
                {
                    Id = 2,
                    Name = "Master Chief"
                },
                new Position
                {
                    Id = 3,
                    Name = "Master"
                });

            //context.Employees.AddOrUpdate(
            //    i => i.Id,
            //    new Employee
            //    {
            //        Name = "First",
            //        DateBirth = timestamp,
            //        PositionId = 2,
            //        ChiefEmployeeId = 2,
            //        EmployeeCVId = 1
            //    },
            //    new Employee
            //    {
            //        Name = "Second",
            //        DateBirth = timestamp,
            //        PositionId = 1,
            //        ChiefEmployeeId = 2,
            //        EmployeeCVId = 2
            //    },
            //    new Employee
            //    {
            //        Name = "Third",
            //        DateBirth = timestamp,
            //        PositionId = 3,
            //        ChiefEmployeeId = 1,
            //        EmployeeCVId = 3
            //    });

            //context.EmployeeCVs.AddOrUpdate(
            //    i => i.Id,
            //    new EmployeeCV
            //    {
            //        Id = 1,
            //        File = ReturnBytes("1.docx"),
            //        Name = "1.docx",
            //        LastUpdate = timestamp
            //    },
            //    new EmployeeCV
            //    {
            //        Id = 2,
            //        File = ReturnBytes("2.docx"),
            //        Name = "2.docx",
            //        LastUpdate = timestamp
            //    },
            //    new EmployeeCV
            //    {
            //        Id = 3,
            //        File = ReturnBytes("3.doc"),
            //        Name = "3.doc",
            //        LastUpdate = timestamp
            //    },
            //    new EmployeeCV
            //    {
            //        Id = 4,
            //        File = ReturnBytes("4.docx"),
            //        Name = "4.docx",
            //        LastUpdate = timestamp
            //    },
            //    new EmployeeCV
            //    {
            //        Id = 5,
            //        File = ReturnBytes("5.docx"),
            //        Name = "5.docx",
            //        LastUpdate = timestamp
            //    });

            //context.Orders.AddOrUpdate(
            //        i => i.Id,
            //        new Order
            //        {
            //            Id = 1,
            //            CustomerId = 2,
            //            EmployeeId = 3,
            //            OrderAcceptanceDate = timestamp,
            //            OrderDispatchDate = timestamp
            //        },
            //        new Order
            //        {
            //            Id = 2,
            //            CustomerId = 2,
            //            EmployeeId = 4,
            //            OrderAcceptanceDate = timestamp,
            //            OrderDispatchDate = timestamp
            //        });

            //context.OrderProducts.AddOrUpdate(
            //    i => i.Id,
            //    new OrderProduct
            //    {
            //        Id = 1,
            //        OrderId = 2,
            //        StoredItemId = 3
            //    },
            //    new OrderProduct
            //    {
            //        Id = 2,
            //        OrderId = 3,
            //        StoredItemId = 1
            //    },
            //    new OrderProduct
            //    {
            //        Id = 3,
            //        OrderId = 4,
            //        StoredItemId = 2
            //    },
            //    new OrderProduct
            //    {
            //        Id = 4,
            //        OrderId = 1,
            //        StoredItemId = 4
                //});
        }

        private byte[] ReturnBytes(string fileName)
        {
            byte[] file;
            using (FileStream fstream = new FileStream(@"d:\work\employeecv\" + fileName, FileMode.Open))
            {
                byte[] bytes = new byte[fstream.Length];
                fstream.Read(bytes, 0, bytes.Length);
                file = bytes;
            }

            return file;
        }
    }
}
