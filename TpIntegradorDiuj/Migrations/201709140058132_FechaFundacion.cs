namespace TpIntegradorDiuj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FechaFundacion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Empresas", "FechaFundacion", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Empresas", "FechaFundacion");
        }
    }
}
