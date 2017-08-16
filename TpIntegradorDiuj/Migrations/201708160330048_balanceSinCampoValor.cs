namespace TpIntegradorDiuj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class balanceSinCampoValor : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Balances", "Valor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Balances", "Valor", c => c.Double(nullable: false));
        }
    }
}
