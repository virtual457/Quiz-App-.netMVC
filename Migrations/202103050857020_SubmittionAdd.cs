namespace QuizApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubmittionAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Submittions",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        SubmittedAnswer = c.String(),
                        Result = c.String(),
                        SubmittedBy = c.String(),
                        SubmittedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Submittions");
        }
    }
}
