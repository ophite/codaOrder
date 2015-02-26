using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace iOrder.Models
{
    public class IdentityContext : DbContext
    {
        public IdentityContext()
            : base("name=identityConnection")
        {

        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UsersInRole> UsersInRoles { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<OAuthMembership> OAuthMemberships { get; set; }
        public DbSet<Role> Roles { get; set; }
        
        public static void InitializeDatabase()
        {
            var cs = System.Configuration.ConfigurationManager.ConnectionStrings["identityConnection"];
            WebSecurity.InitializeDatabaseConnection(cs.Name, "UserProfile", "UserID", "UserName", true);
            //// Codajson
            //            CREATE TABLE CodaJson
            //(
            //OID          bigint NOT NULL PRIMARY KEY,
            //FullName		varchar (128) NOT NULL 
            //)
        }
    }
}