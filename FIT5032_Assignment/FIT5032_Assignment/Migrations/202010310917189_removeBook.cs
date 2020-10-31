namespace FIT5032_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeBook : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookEvents", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BookEvents", "BookingId", "dbo.Bookings");
            DropIndex("dbo.BookEvents", new[] { "BookingId" });
            DropIndex("dbo.BookEvents", new[] { "ApplicationUserId" });
            DropTable("dbo.BookEvents");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BookEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookingId = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.BookEvents", "ApplicationUserId");
            CreateIndex("dbo.BookEvents", "BookingId");
            AddForeignKey("dbo.BookEvents", "BookingId", "dbo.Bookings", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BookEvents", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
