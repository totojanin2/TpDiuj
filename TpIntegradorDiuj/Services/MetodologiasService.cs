using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TpIntegradorDiuj.Models;

namespace TpIntegradorDiuj.Services
{
    public class MetodologiasService
    {
        TpIntegradorDbContext db;
        EmpresasService empService;
        public MetodologiasService(TpIntegradorDbContext db)
        {
            this.db = db;
            empService = new EmpresasService(db);
        }
        public List<Metodologia> GetAll()
        {
            return db.Metodologias.ToList();
        }
        public Metodologia GetById(int id)
        {
            return db.Metodologias.FirstOrDefault(x => x.Id == id);
        }
        public void Agregar(Metodologia model, List<int> idsCondiciones)
        {
            int[] arrayIds = idsCondiciones.ToArray();
            List<Condicion> condicionesAAgregar = db.Condiciones.Where(x => arrayIds.Contains(x.Id)).ToList();
            model.Condiciones.AddRange(condicionesAAgregar);
            db.Metodologias.Add(model);
            db.SaveChanges();
        }

        public bool EvaluarConvenienciaInversion(string empresaCuit, int metodologiaId)
        {
            //Obtengo la empresa solicitada
            Empresa empresa = empService.GetByCUIT(empresaCuit);
            //Obtengo la metodologia solicitada
            Metodologia metodologia = this.GetById(metodologiaId);
            //Ejecuto las condiciones de la metodología, para tal empresa, para ver si conviene invertir o no
            bool result = metodologia.EsDeseableInvertir(empresa, db.Operandos.ToList());
            return result;
        }

        public void Eliminar(int id)
        {
            var met = db.Metodologias.FirstOrDefault(x => x.Id == id);
            db.Metodologias.Remove(met);
            db.SaveChanges();
        }
    }
}