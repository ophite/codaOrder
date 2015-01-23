namespace WebApplication3.IdentityContextMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfile", "FirstName", c => c.String());
            AddColumn("dbo.UserProfile", "LastName", c => c.String());
            AddColumn("dbo.UserProfile", "EmailAddress", c => c.String());
            AddColumn("dbo.UserProfile", "IsEnabled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfile", "IsEnabled");
            DropColumn("dbo.UserProfile", "EmailAddress");
            DropColumn("dbo.UserProfile", "LastName");
            DropColumn("dbo.UserProfile", "FirstName");
        }
    }
}
