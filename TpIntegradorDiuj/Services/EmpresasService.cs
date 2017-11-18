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
        public static Empresa GetById(int id)
        {
            TpIntegradorDbContext db = new TpIntegradorDbContext();
            return db.Empresas.FirstOrDefault(x => x.Id == id);
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
            Empresa empresaOriginal = EmpresasService.GetById(empresaEditada.Id);
            empresaOriginal.Editar(empresaEditada);
            db.SaveChanges();
        }
        public static void Eliminar(int id)
        {
            TpIntegradorDbContext db = new TpIntegradorDbContext();
            Empresa empresaAEliminar = EmpresasService.GetById(id);
            db.Empresas.Remove(empresaAEliminar);
            db.SaveChanges();
        }
    }
}