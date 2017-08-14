namespace TpIntegradorDiuj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class estructuraInicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Balances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Periodo = c.Int(nullable: false),
                        Empresa_Id = c.Int(nullable: false),
                        Valor = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empresas", t => t.Empresa_Id, cascadeDelete: true)
                .Index(t => t.Empresa_Id);
            
            CreateTable(
                "dbo.Operandos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IndicadorPadre_Id = c.Int(),
                        Nombre = c.String(),
                        Balance_Id = c.Int(),
                        Valor = c.Double(),
                        Formula = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Balances", t => t.Balance_Id, cascadeDelete: true)
                .ForeignKey("dbo.Operandos", t => t.IndicadorPadre_Id)
                .Index(t => t.IndicadorPadre_Id)
                .Index(t => t.Balance_Id);
            
            CreateTable(
                "dbo.Condiciones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Indicador_Id = c.Int(),
                        Descripcion = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Operandos", t => t.Indicador_Id)
                .Index(t => t.Indicador_Id);
            
            CreateTable(
                "dbo.Empresas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Metodologias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MetodologiaCondicions",
                c => new
                    {
                        Metodologia_Id = c.Int(nullable: false),
                        Condicion_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Metodologia_Id, t.Condicion_Id })
                .ForeignKey("dbo.Metodologias", t => t.Metodologia_Id, cascadeDelete: true)
                .ForeignKey("dbo.Condiciones", t => t.Condicion_Id, cascadeDelete: true)
                .Index(t => t.Metodologia_Id)
                .Index(t => t.Condicion_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MetodologiaCondicions", "Condicion_Id", "dbo.Condiciones");
            DropForeignKey("dbo.MetodologiaCondicions", "Metodologia_Id", "dbo.Metodologias");
            DropForeignKey("dbo.Balances", "Empresa_Id", "dbo.Empresas");
            DropForeignKey("dbo.Condiciones", "Indicador_Id", "dbo.Operandos");
            DropForeignKey("dbo.Operandos", "IndicadorPadre_Id", "dbo.Operandos");
            DropForeignKey("dbo.Operandos", "Balance_Id", "dbo.Balances");
            DropIndex("dbo.MetodologiaCondicions", new[] { "Condicion_Id" });
            DropIndex("dbo.MetodologiaCondicions", new[] { "Metodologia_Id" });
            DropIndex("dbo.Condiciones", new[] { "Indicador_Id" });
            DropIndex("dbo.Operandos", new[] { "Balance_Id" });
            DropIndex("dbo.Operandos", new[] { "IndicadorPadre_Id" });
            DropIndex("dbo.Balances", new[] { "Empresa_Id" });
            DropTable("dbo.MetodologiaCondicions");
            DropTable("dbo.Metodologias");
            DropTable("dbo.Empresas");
            DropTable("dbo.Condiciones");
            DropTable("dbo.Operandos");
            DropTable("dbo.Balances");
        }
    }
}
