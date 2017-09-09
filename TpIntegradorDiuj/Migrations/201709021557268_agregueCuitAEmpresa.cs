namespace TpIntegradorDiuj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agregueCuitAEmpresa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Empresas", "CUIT", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Empresas", "CUIT");
        }
    }
}
