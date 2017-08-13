using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TpIntegradorDiuj.Models;

namespace TpIntegradorDiuj
{
    public class TpIntegradorDbContext : DbContext
    {
          public TpIntegradorDbContext() : base("DefaultConnection")
        {
            this.Configuration.ValidateOnSaveEnabled = false;
            this.Database.CommandTimeout = 360;
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }
        public virtual DbSet<Empresa> Empresas { get; set; }
        public virtual DbSet<Cuenta> Cuentas { get; set; }
        public virtual DbSet<Balance> Balances { get; set; }
        public virtual DbSet<Metodologia> Metodologias { get; set; }
        public virtual DbSet<Condicion> Condiciones { get; set; }
        public virtual DbSet<Indicador> Indicadores { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<>
        }
    }
}