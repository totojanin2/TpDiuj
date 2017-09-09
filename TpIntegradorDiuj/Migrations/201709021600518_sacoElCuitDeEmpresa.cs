namespace TpIntegradorDiuj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sacoElCuitDeEmpresa : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Empresas", "CUIT");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Empresas", "CUIT", c => c.String());
        }
    }
}
