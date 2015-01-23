namespace WebApplication3.IdentityContextMigration
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebApplication3.Models;
    using WebMatrix.WebData;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication3.Models.IdentityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"IdentityContextMigration";
        }

        protected override void Seed(WebApplication3.Models.IdentityContext context)
        {
            //context.UserProfiles.AddOrUpdate(
            //    u => u.UserName,
            //    new UserProfile { UserName = "admin" }
            //);

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
            //

            SeedMembership();
        }

        private void SeedMembership()
        {
            IdentityContext.InitializeDatabase();
            var roles = (SimpleRoleProvider)Roles.Provider;
            var memberships = (SimpleMembershipProvider)System.Web.Security.Membership.Provider;

            if (!roles.RoleExists("admin"))
                roles.CreateRole("admin");

            if (memberships.GetUser("coda admin", false) == null)
                memberships.CreateUserAndAccount("coda admin", "1111");

            if (!roles.GetRolesForUser("coda admin").Contains("admin"))
                roles.AddUsersToRoles(new string[] { "coda admin" }, new string[] { "admin" });
        }
    }
}
