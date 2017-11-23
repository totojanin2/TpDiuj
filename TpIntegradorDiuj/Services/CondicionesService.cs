using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TpIntegradorDiuj.Models;

namespace TpIntegradorDiuj.Services
{
    public class CondicionesService
    {
        public static List<Condicion> GetAll()
        {
           TpIntegradorDbContext db = new TpIntegradorDbContext();
           return db.Condiciones.Include("Indicador").ToList();
        }
        public static void Crear(Condicion condicion)
        {
            TpIntegradorDbContext db = new TpIntegradorDbContext();
            db.Condiciones.Add(condicion);
            db.SaveChanges();
        }
    }
}