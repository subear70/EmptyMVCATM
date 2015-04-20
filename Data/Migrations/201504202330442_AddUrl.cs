namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CardInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CardNumber = c.String(),
                        PinCode = c.String(),
                        Balance = c.Long(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LogInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CardId = c.Long(nullable: false),
                        OperationCode = c.Int(nullable: false),
                        OperationValue = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LogInfoes");
            DropTable("dbo.CardInfoes");
        }
    }
}
