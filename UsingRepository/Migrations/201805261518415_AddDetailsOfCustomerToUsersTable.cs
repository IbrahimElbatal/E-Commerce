namespace UsingRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDetailsOfCustomerToUsersTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(maxLength: 255));
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(maxLength: 255));
            AddColumn("dbo.AspNetUsers", "Fax", c => c.String(maxLength: 255));
            AddColumn("dbo.AspNetUsers", "Address", c => c.String(maxLength: 255));
            AddColumn("dbo.AspNetUsers", "PostedCode", c => c.String(maxLength: 255));
            AddColumn("dbo.AspNetUsers", "CityId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "CountryId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "CityId");
            CreateIndex("dbo.AspNetUsers", "CountryId");
            AddForeignKey("dbo.AspNetUsers", "CityId", "dbo.Cities", "Id");
            AddForeignKey("dbo.AspNetUsers", "CountryId", "dbo.Countries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.AspNetUsers", "CityId", "dbo.Cities");
            DropIndex("dbo.AspNetUsers", new[] { "CountryId" });
            DropIndex("dbo.AspNetUsers", new[] { "CityId" });
            DropColumn("dbo.AspNetUsers", "CountryId");
            DropColumn("dbo.AspNetUsers", "CityId");
            DropColumn("dbo.AspNetUsers", "PostedCode");
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "Fax");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
