using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class IdentityContext : DbContext
    {
        public IdentityContext()
            : base("name=identityConnection")
        {

        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}