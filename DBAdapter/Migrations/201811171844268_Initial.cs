using System.Data.Entity.Migrations;

namespace Lab1.DBAdapter.Migrations
{
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        FolderPath = c.String(nullable: false),
                        FilesCount = c.Int(nullable: false),
                        RequestDate = c.Int(nullable: false),
                        FullVolume = c.Double(nullable: false),
                        CurrentExtension = c.Int(nullable: false),
                        RequestDate1 = c.DateTime(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Guid)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(),
                        Login = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        LastLoginDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Guid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "UserId", "dbo.Users");
            DropIndex("dbo.Requests", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Requests");
        }
    }
}
