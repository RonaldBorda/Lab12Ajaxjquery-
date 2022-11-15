namespace CodeFirstDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambios : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sellers",
                c => new
                    {
                        SellerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LastName = c.String(),
                        Area = c.String(),
                    })
                .PrimaryKey(t => t.SellerId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sellers");
        }
    }
}
