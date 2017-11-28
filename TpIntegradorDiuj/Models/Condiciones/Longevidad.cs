using System;
using System.Collections.Generic;

namespace TpIntegradorDiuj.Models.Condiciones
{
    public class Longevidad : Condicion
    {
        public override bool Analizar(Empresa empresa, List<ComponenteOperando> lista)
        {
            //sólo vale la pena invertir en empresas con más de 10 años.
            return empresa.FechaFundacion <= DateTime.Now.AddYears(-10);
        }
    }
}