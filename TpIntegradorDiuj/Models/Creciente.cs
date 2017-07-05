using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TpIntegradorDiuj.Models
{
    public class Creciente : ICondicion
    {
        public Indicador Indicador { get; set; }
        public int Indicador_Id { get; set; }
        public string Descripcion { get; set; }

        public bool Analizar(Empresa empresa)
        {
            List<int> Periodos = new List<int>();
            bool result = true;
            int i;
            for ( i = 2013; i <= 2017; i++)
                Periodos.Add(i);
            i = 0;
            while(i<Periodos.Count && result)
            {
                result= this.Indicador.ObtenerValor(empresa, i) < this.Indicador.ObtenerValor(empresa, i + 1);
            }
            return result;
        }
    }
}