using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TpIntegradorDiuj.Models
{
    public class MargenesCreciente : Condicion
    {
        public MargenesCreciente()
        {
        }

        public MargenesCreciente(string descripcion,int indicador_id) :base(descripcion,indicador_id)
        {

        }
        public override bool Analizar(Empresa empresa)
        {
            bool result = true;
            List<int> periodos = this.ObtenerPeriodosAConsultar(empresa);
            int i=0;        
            while(i<periodos.Count && result)
            {
                result= this.Indicador.ObtenerValor(empresa, i) < this.Indicador.ObtenerValor(empresa, i + 1);
                i++;
            }
            return result;
        }
    }
}