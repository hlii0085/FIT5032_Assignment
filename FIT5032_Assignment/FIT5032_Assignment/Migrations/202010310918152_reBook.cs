namespace FIT5032_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reBook : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookingId = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Bookings", t => t.BookingId, cascadeDelete: true)
                .Index(t => t.BookingId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookEvents", "BookingId", "dbo.Bookings");
            DropForeignKey("dbo.BookEvents", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.BookEvents", new[] { "ApplicationUserId" });
            DropIndex("dbo.BookEvents", new[] { "BookingId" });
            DropTable("dbo.BookEvents");
        }
    }
}
