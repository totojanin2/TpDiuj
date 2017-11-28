using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TpIntegradorDiuj.Models;

namespace TpIntegradorDiuj.Services
{
    
    public class EmpresasService
    {
        public static List<Empresa> GetAll()
        {
            TpIntegradorDbContext db = new TpIntegradorDbContext();
            return db.Empresas.ToList();
        }
        public static Empresa GetByCUIT(string cuit)
        {
            TpIntegradorDbContext db = new TpIntegradorDbContext();
            return db.Empresas.FirstOrDefault(x => x.CUIT == cuit);
        }
        public static void Crear(Empresa empresa)
        {
            TpIntegradorDbContext db = new TpIntegradorDbContext();
            db.Empresas.Add(empresa);
            db.SaveChanges();
        }
        public static void Editar(Empresa empresaEditada)
        {
            TpIntegradorDbContext db = new TpIntegradorDbContext();
            Empresa empresaOriginal = EmpresasService.GetByCUIT(empresaEditada.CUIT);
            empresaEditada.Balances = empresaOriginal.Balances;
            db.Entry(empresaEditada).State = System.Data.Entity.EntityState.Modified;
            //empresaOriginal.Editar(empresaEditada);
            db.SaveChanges();
        }
        public static void Eliminar(string cuit)
        {
            TpIntegradorDbContext db = new TpIntegradorDbContext();
            Empresa empresaAEliminar = EmpresasService.GetByCUIT(cuit);
            db.Empresas.Remove(empresaAEliminar);
            db.SaveChanges();
        }
    }
}