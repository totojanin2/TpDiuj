using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TpIntegradorDiuj.Models
{
    public class Metodologia
    {
        public int Id { get; set; }
        public int Nombre { get; set; }
        //TODO: Fijarse si hay que agregar los indicadores o no
        public virtual List<Condicion> Condiciones{get;set;}
        public bool EsDeseableInvertir(Empresa emp)
        {
            //Me fijo que se cumplan todas las condiciones de esta metodologia
            return this.Condiciones.All(x => x.Analizar(emp));
        }
    }
}