namespace UsingRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsDeletedColumnToSliderAndProductDiscountTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductDiscounts", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Sliders", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sliders", "IsDeleted");
            DropColumn("dbo.ProductDiscounts", "IsDeleted");
        }
    }
}
