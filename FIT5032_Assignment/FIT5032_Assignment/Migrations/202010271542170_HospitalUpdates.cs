namespace FIT5032_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HospitalUpdates : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Hospitals", "Lattitude");
            DropColumn("dbo.Hospitals", "Longitude");
            DropColumn("dbo.Hospitals", "PostCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Hospitals", "PostCode", c => c.Int(nullable: false));
            AddColumn("dbo.Hospitals", "Longitude", c => c.String());
            AddColumn("dbo.Hospitals", "Lattitude", c => c.String());
        }
    }
}
