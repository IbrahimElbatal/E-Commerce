namespace UsingRepository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedUsersAndRoles : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [LastName], [Fax], [Address], [PostedCode], [CityId], [CountryId]) VALUES (N'00a4ef2c-cd66-4285-82fa-539ed7dded52', N'ibrahimelbatal2@gmail.com', 1, N'APnrqRWTTKrsDn9yOs45S+e9yAzmVtiOesZFgUg77ASi40Vbcpqf0RXq3/F/xU0iPA==', N'bfd3139d-9fc3-413b-bb23-575e22988dcf', N'01022169733', 1, 0, NULL, 1, 0, N'Ibrahim Elbatal', N'Ibrahim', N'Elbatal', N'+20', N'cairo', N'31111', 1, 1)
                
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'97c37e85-fd55-4f31-9b6c-dd97c3bf2b3d', N'Adminstrator')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'cc239247-123a-4c15-9b91-6aace5c2d6cb', N'Create')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'9401f45c-724c-4082-ad93-0a779032e260', N'Delete')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'73f12f79-85c9-40bb-aedd-042b9f0e9d9f', N'Edit')

                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'00a4ef2c-cd66-4285-82fa-539ed7dded52', N'73f12f79-85c9-40bb-aedd-042b9f0e9d9f')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'00a4ef2c-cd66-4285-82fa-539ed7dded52', N'9401f45c-724c-4082-ad93-0a779032e260')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'00a4ef2c-cd66-4285-82fa-539ed7dded52', N'97c37e85-fd55-4f31-9b6c-dd97c3bf2b3d')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'00a4ef2c-cd66-4285-82fa-539ed7dded52', N'cc239247-123a-4c15-9b91-6aace5c2d6cb')

                INSERT INTO [dbo].[AspNetUserLogins] ([LoginProvider], [ProviderKey], [UserId]) VALUES (N'Google', N'109601616628117903445', N'00a4ef2c-cd66-4285-82fa-539ed7dded52')
                INSERT INTO [dbo].[AspNetUserLogins] ([LoginProvider], [ProviderKey], [UserId]) VALUES (N'Microsoft', N'cba108689075d30f', N'00a4ef2c-cd66-4285-82fa-539ed7dded52')

                ");
        }

        public override void Down()
        {
        }
    }
}
