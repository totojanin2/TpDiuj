using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TpIntegradorDiuj.Models;
using TpIntegradorDiuj.Models.Condiciones;

namespace TpIntegradorDiuj
{

    public class TpIntegradorDbContext : DbContext
    {
        private static TpIntegradorDbContext Instance = null;
        public TpIntegradorDbContext() : base("DefaultConnection")
        {
            this.Configuration.ValidateOnSaveEnabled = false;
            this.Database.CommandTimeout = 360;
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
           // this.AgregarCondiciones();
        }       
        public virtual DbSet<Empresa> Empresas { get; set; }
        public virtual DbSet<Cuenta> Cuentas { get; set; }
        public virtual DbSet<Balance> Balances { get; set; }
        public virtual DbSet<Metodologia> Metodologias { get; set; }
        public virtual DbSet<Condicion> Condiciones { get; set; }
        public virtual DbSet<Indicador> Indicadores { get; set; }

        private void AgregarCondiciones()
        {
            List<Condicion> condiciones = this.Condiciones.ToList();
            this.Condiciones.RemoveRange(condiciones);
            this.SaveChanges();
            Condicion creciente = new Creciente()
            {
                Descripcion = "Que los margenes de beneficio sean crecientes",
                Indicador_Id = this.Indicadores.ToList().FirstOrDefault(x => x.Nombre.ToLower().Contains("margenes")).Id               
            };
            Condicion roeConsistente = new RoeConsistente()
            {
                Descripcion = "La ROE tiene que ser consistente",
                Indicador_Id = this.Indicadores.ToList().FirstOrDefault(x => x.Nombre.ToLower().Contains("roe")).Id
            };
            this.Condiciones.Add(roeConsistente);
            this.Condiciones.Add(creciente);
            this.SaveChanges();
        }
        public static TpIntegradorDbContext GetInstance()
        {
            if (Instance == null)
                Instance = new TpIntegradorDbContext();
            return Instance;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empresa>()
                .ToTable("Empresas")
                .HasKey(x => x.Id);

            modelBuilder.Entity<Empresa>()
                .HasMany(x => x.Balances).WithRequired().HasForeignKey(x => x.Empresa_Id);

            modelBuilder.Entity<Balance>()
                .ToTable("Balances")
                .HasKey(x => x.Id);
               // .HasRequired(x => x.Empresa).WithMany().HasForeignKey(x => x.Empresa_Id);

            modelBuilder.Entity<Balance>().HasMany(x => x.Cuentas).WithRequired().HasForeignKey(x => x.Balance_Id);

            modelBuilder.Entity<Indicador>()
               // .ToTable("Indicadores")
                .HasKey(x => x.Id)
                  .HasMany(x => x.Operandos).WithOptional().HasForeignKey(x => x.IndicadorPadre_Id);

            modelBuilder.Entity<Metodologia>().ToTable("Metodologias").HasKey(x => x.Id);
            modelBuilder.Entity<Metodologia>().HasMany(x => x.Condiciones).WithMany();

            modelBuilder.Entity<Cuenta>()
               // .ToTable("Cuentas")
                .HasKey(x => x.Id);

            modelBuilder.Entity<Condicion>().ToTable("Condiciones")
                .HasKey(x => x.Id)
                .HasOptional(x => x.Indicador).WithMany().HasForeignKey(x => x.Indicador_Id);

            modelBuilder.Entity<ComponenteOperando>().ToTable("Operandos")
               .HasKey(x => x.Id);

        }
    }
}