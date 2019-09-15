namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    /* "add-migration SeedUsers"
     */

    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            // insert-käskyt kopioitu tietokannasta, 1.-2. aspnetusers -> show table -> valitse rivit -> klikkaa oikealla script
            // 3. aspnetroles-taulusta
            // 4. aspnetuserroles-taulusta (tätä ei tyhjennetä kopioinnin jälkeen)
            // käskyjen kopioinnin jälkeen tiedot poistettu tauluista aspnetusers & aspnetroles, sitten "update-database"
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'8fb3cbe7-a1e9-42d4-a47d-414d3eda4837', N'guest@vidly.com', 0, N'ANS03TU5dPxgZyD3qzXX/++gzag0N371ShxJ59Jhy9ggRy3jFyVjBdCrFwnlvUoWAg==', N'ad962423-b685-4df5-9402-cdf24a72cbc2', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'd94f572e-bda3-4b5c-8492-3f41a3576f72', N'admin@vidly.com', 0, N'AB2HhtD/3JEdaOAECSHY1hKHdYLn9DB2zx7fi1RF6eOyxGVkCmEeSlvaOmfpS4yUmA==', N'741d3a62-d737-41bf-bc34-a7211db638b5', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'2f910a04-29b7-4ba5-a0fd-c8e0e7605f0a', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd94f572e-bda3-4b5c-8492-3f41a3576f72', N'2f910a04-29b7-4ba5-a0fd-c8e0e7605f0a')
");
        }
        
        public override void Down()
        {
        }
    }
}
