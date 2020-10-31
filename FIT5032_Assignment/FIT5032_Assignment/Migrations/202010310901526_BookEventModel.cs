namespace FIT5032_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookEventModel : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.BookingByTimes", newName: "BookEvents");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.BookEvents", newName: "BookingByTimes");
        }
    }
}
