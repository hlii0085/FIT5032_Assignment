namespace FIT5032_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StoryModelChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Stories", "Caption", c => c.String(nullable: false));
            AlterColumn("dbo.Stories", "Description", c => c.String(nullable: false));
            DropColumn("dbo.Stories", "Time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stories", "Time", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Stories", "Description", c => c.String());
            AlterColumn("dbo.Stories", "Caption", c => c.String());
        }
    }
}
