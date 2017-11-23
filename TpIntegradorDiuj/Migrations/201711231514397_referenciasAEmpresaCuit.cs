namespace TpIntegradorDiuj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class referenciasAEmpresaCuit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Balances", "Empresa_Id", "dbo.Empresas");
            DropIndex("dbo.Balances", new[] { "Empresa_Id" });
            RenameColumn(table: "dbo.Balances", name: "Empresa_Id", newName: "Empresa_CUIT");
            DropPrimaryKey("dbo.Empresas");
            AlterColumn("dbo.Balances", "Empresa_CUIT", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Empresas", "CUIT", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Empresas", "CUIT");
            Sql(@"update dbo.Balances
                set Empresa_CUIT = '20334566550'");
            CreateIndex("dbo.Balances", "Empresa_CUIT");
            AddForeignKey("dbo.Balances", "Empresa_CUIT", "dbo.Empresas", "CUIT", cascadeDelete: true);
            DropColumn("dbo.Empresas", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Empresas", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Balances", "Empresa_CUIT", "dbo.Empresas");
            DropIndex("dbo.Balances", new[] { "Empresa_CUIT" });
            DropPrimaryKey("dbo.Empresas");
            AlterColumn("dbo.Empresas", "CUIT", c => c.String());
            AlterColumn("dbo.Balances", "Empresa_CUIT", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Empresas", "Id");
            RenameColumn(table: "dbo.Balances", name: "Empresa_CUIT", newName: "Empresa_Id");
            CreateIndex("dbo.Balances", "Empresa_Id");
            AddForeignKey("dbo.Balances", "Empresa_Id", "dbo.Empresas", "Id", cascadeDelete: true);
        }
    }
}
