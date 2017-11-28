using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TpIntegradorDiuj.Models;

namespace TpIntegradorDiuj.Services
{
    public class CondicionesService
    {
        TpIntegradorDbContext db;
        public CondicionesService(TpIntegradorDbContext _db)
        {
            this.db = _db;
        }
        public List<Condicion> GetAll()
        {
           return db.Condiciones.Include("Indicador").ToList();
        }
        public void Crear(Condicion condicion)
        {
            db.Condiciones.Add(condicion);
            db.SaveChanges();
        }
    }
}