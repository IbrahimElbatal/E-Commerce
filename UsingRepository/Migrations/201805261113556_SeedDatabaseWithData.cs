namespace UsingRepository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedDatabaseWithData : DbMigration
    {
        public override void Up()
        {
            Sql("Insert Into Countries (Name) VALUES ('Egypt')");
            Sql("Insert Into Countries (Name) VALUES ('Germany')");
            Sql("Insert Into Countries (Name) VALUES ('Australia')");

            Sql("Insert Into Cities (Name , CountryId) VALUES ('Cairo',(Select ID From Countries Where Name='Egypt'))");
            Sql("Insert Into Cities (Name , CountryId) VALUES ('Alex',(Select ID From Countries Where Name='Egypt'))");
            Sql("Insert Into Cities (Name , CountryId) VALUES ('Giza',(Select ID From Countries Where Name='Egypt'))");

            Sql("Insert Into Cities (Name , CountryId) VALUES ('Berlin',(Select ID From Countries Where Name='Germany'))");
            Sql("Insert Into Cities (Name , CountryId) VALUES ('Hamburg',(Select ID From Countries Where Name='Germany'))");
            Sql("Insert Into Cities (Name , CountryId) VALUES ('Hesse',(Select ID From Countries Where Name='Germany'))");

            Sql("Insert Into Cities (Name , CountryId) VALUES ('South Australia',(Select ID From Countries Where Name='Australia'))");
            Sql("Insert Into Cities (Name , CountryId) VALUES ('Western Australia',(Select ID From Countries Where Name='Australia'))");
            Sql("Insert Into Cities (Name , CountryId) VALUES ('Queenland',(Select ID From Countries Where Name='Australia'))");
        }

        public override void Down()
        {
            Sql("DELETE FROM Countries");
        }
    }
}
