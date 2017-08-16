using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpIntegradorDiuj.Models
{
   public abstract class Condicion
    {
        public int Id { get; set; }
        public virtual Indicador Indicador { get;set; }
        public int? Indicador_Id { get; set; }
        public string Descripcion { get; set; }
        //Cada condicion será una clase, que tiene que implementar su metodo "ANALIZAR"
        public abstract bool Analizar(Empresa empresa);
    }
}
