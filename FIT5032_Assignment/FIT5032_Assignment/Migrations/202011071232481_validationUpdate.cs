namespace FIT5032_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validationUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Hospitals", "Description", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Hospitals", "Address", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Hospitals", "Address", c => c.String());
            AlterColumn("dbo.Hospitals", "Description", c => c.String());
        }
    }
}
