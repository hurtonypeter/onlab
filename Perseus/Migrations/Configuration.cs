namespace Perseus.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Perseus.DataModel;

    internal sealed class Configuration : DbMigrationsConfiguration<Perseus.DataModel.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            // adminadmin hash: AJJtHhAT2GZxPUxTb1wdkMw20xZmsLzsYyIfe1iNBvfoGu6prou2tMizEPoeha5xWg==
            /*context.Users.AddOrUpdate(
                new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "admin",
                    PasswordHash = "AHz+NGSIjNkILlzBu1EPM5+0mw1w4QoPOjhu+C4PlfWUqiZpECtGJlMwHQaX0UDzFQ==",
                    Created = DateTime.Now,
                    Status = false,
                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0
                },
                new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "admin1",
                    PasswordHash = "AHz+NGSIjNkILlzBu1EPM5+0mw1w4QoPOjhu+C4PlfWUqiZpECtGJlMwHQaX0UDzFQ==",
                    Created = DateTime.Now,
                    Status = false,
                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0
                },
                new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "admin2",
                    PasswordHash = "AHz+NGSIjNkILlzBu1EPM5+0mw1w4QoPOjhu+C4PlfWUqiZpECtGJlMwHQaX0UDzFQ==",
                    Created = DateTime.Now,
                    Status = false,
                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0
                }
            );
            context.Roles.AddOrUpdate(
                new ApplicationRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "anonymous",
                },
                new ApplicationRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "admin",
                }
            );*/
        }
    }
}
