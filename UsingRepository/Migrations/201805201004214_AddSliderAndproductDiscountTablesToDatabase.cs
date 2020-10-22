namespace UsingRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSliderAndproductDiscountTablesToDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductDiscounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Discount = c.Single(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Sliders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImagePath = c.String(maxLength: 200),
                        Header = c.String(maxLength: 250),
                        Paragraph = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductDiscounts", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductDiscounts", new[] { "ProductId" });
            DropTable("dbo.Sliders");
            DropTable("dbo.ProductDiscounts");
        }
    }
}
