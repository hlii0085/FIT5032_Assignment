namespace FIT5032_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Hospitals", "HospitalName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Stories", "Caption", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stories", "Caption", c => c.String(nullable: false));
            AlterColumn("dbo.Hospitals", "HospitalName", c => c.String());
        }
    }
}
