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

        public virtual List<ICondicion> Condiciones{get;set;}
        public bool EsDeseableInvertir(Empresa emp)
        {
            return this.Condiciones.All(x => x.Analizar(emp));
        }
    }
}