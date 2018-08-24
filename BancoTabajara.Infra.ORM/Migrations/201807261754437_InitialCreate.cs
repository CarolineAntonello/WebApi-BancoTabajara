namespace BancoTabajara.Infra.ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                        CPF = c.String(nullable: false, maxLength: 50),
                        RG = c.String(nullable: false, maxLength: 50),
                        DataNascimento = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumeroConta = c.Int(nullable: false),
                        Saldo = c.Double(nullable: false),
                        Ativada = c.Boolean(nullable: false),
                        Limite = c.Double(nullable: false),
                        Cliente_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.Cliente_Id, cascadeDelete: true)
                .Index(t => t.Cliente_Id);
            
            CreateTable(
                "dbo.Movimentacaos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        Valor = c.Double(nullable: false),
                        TipoOperacao = c.Int(nullable: false),
                        Conta_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contas", t => t.Conta_Id, cascadeDelete: true)
                .Index(t => t.Conta_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movimentacaos", "Conta_Id", "dbo.Contas");
            DropForeignKey("dbo.Contas", "Cliente_Id", "dbo.Clientes");
            DropIndex("dbo.Movimentacaos", new[] { "Conta_Id" });
            DropIndex("dbo.Contas", new[] { "Cliente_Id" });
            DropTable("dbo.Movimentacaos");
            DropTable("dbo.Contas");
            DropTable("dbo.Clientes");
        }
    }
}
