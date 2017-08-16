using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TpIntegradorDiuj.Models
{
    public class Metodologia
    {
        public Metodologia()
        {
            this.Condiciones = new List<Condicion>();
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        //TODO: Fijarse si hay que agregar los indicadores o no
        public virtual List<Condicion> Condiciones{get;set;}
        public bool EsDeseableInvertir(Empresa emp)
        {
            //Me fijo que se cumplan todas las condiciones de esta metodologia
            return this.Condiciones.All(x => x.Analizar(emp));
        }
    }
}