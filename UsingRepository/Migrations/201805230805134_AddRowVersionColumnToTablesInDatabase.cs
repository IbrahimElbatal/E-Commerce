namespace UsingRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRowVersionColumnToTablesInDatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.OrderDetails", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Orders", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Products", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.ProductDiscounts", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Sliders", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sliders", "RowVersion");
            DropColumn("dbo.ProductDiscounts", "RowVersion");
            DropColumn("dbo.Products", "RowVersion");
            DropColumn("dbo.Orders", "RowVersion");
            DropColumn("dbo.OrderDetails", "RowVersion");
            DropColumn("dbo.Categories", "RowVersion");
        }
    }
}
