namespace TpIntegradorDiuj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TipoCondicion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Condiciones", "Tipo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Condiciones", "Tipo");
        }
    }
}
