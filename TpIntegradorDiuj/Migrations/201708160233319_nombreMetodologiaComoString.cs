namespace TpIntegradorDiuj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nombreMetodologiaComoString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Metodologias", "Nombre", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Metodologias", "Nombre", c => c.Int(nullable: false));
        }
    }
}
