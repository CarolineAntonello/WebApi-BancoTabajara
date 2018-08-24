namespace BancoTabajara.Infra.ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BancoTabajara_v2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Clientes", newName: "TBCliente");
            RenameTable(name: "dbo.Contas", newName: "TBConta");
            RenameTable(name: "dbo.Movimentacaos", newName: "TBMovimentacao");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.TBMovimentacao", newName: "Movimentacaos");
            RenameTable(name: "dbo.TBConta", newName: "Contas");
            RenameTable(name: "dbo.TBCliente", newName: "Clientes");
        }
    }
}
