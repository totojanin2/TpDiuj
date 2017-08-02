using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpIntegradorDiuj.Models
{
   public interface ICondicion
    {
        Indicador Indicador { get;set; }
        int Indicador_Id { get; set; }
        string Descripcion { get; set; }
        //Cada condicion será una clase, que tiene que implementar su metodo "ANALIZAR"
        bool Analizar(Empresa empresa);
    }
}
