using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TpIntegradorDiuj.Models;

namespace TpIntegradorDiuj.Services
{
    
    public class EmpresasService
    {
        private TpIntegradorDbContext db;

        public EmpresasService(TpIntegradorDbContext _db)
        {
            this.db = _db;
        }

        public List<Empresa> GetAll()
        {
            return db.Empresas.ToList();
        }
        public  Empresa GetByCUIT(string cuit)
        {
            return db.Empresas.FirstOrDefault(x => x.CUIT == cuit);
        }
        public  void Crear(Empresa empresa)
        {
            db.Empresas.Add(empresa);
            db.SaveChanges();
        }
        public  void Editar(Empresa empresaEditada)
        {
            Empresa empresaOriginal = GetByCUIT(empresaEditada.CUIT);
            empresaOriginal.Editar(empresaEditada);
            db.SaveChanges();
        }
        public void Eliminar(string cuit)
        {
            Empresa empresaAEliminar = GetByCUIT(cuit);
            db.Empresas.Remove(empresaAEliminar);
            db.SaveChanges();
        }
    }
}