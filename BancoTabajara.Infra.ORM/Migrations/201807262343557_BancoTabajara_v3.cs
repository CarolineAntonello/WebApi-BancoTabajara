namespace BancoTabajara.Infra.ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BancoTabajara_v3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TBMovimentacao", "Conta_Id", "dbo.TBConta");
            DropIndex("dbo.TBMovimentacao", new[] { "Conta_Id" });
            AlterColumn("dbo.TBMovimentacao", "Conta_Id", c => c.Int());
            CreateIndex("dbo.TBMovimentacao", "Conta_Id");
            AddForeignKey("dbo.TBMovimentacao", "Conta_Id", "dbo.TBConta", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBMovimentacao", "Conta_Id", "dbo.TBConta");
            DropIndex("dbo.TBMovimentacao", new[] { "Conta_Id" });
            AlterColumn("dbo.TBMovimentacao", "Conta_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.TBMovimentacao", "Conta_Id");
            AddForeignKey("dbo.TBMovimentacao", "Conta_Id", "dbo.TBConta", "Id", cascadeDelete: true);
        }
    }
}
