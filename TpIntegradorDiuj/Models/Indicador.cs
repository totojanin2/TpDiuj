using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TpIntegradorDiuj.Models
{
    public class Indicador : ComponenteOperando
    {
        public int Id { get; set; }
        public string Formula { get; set; }
        public List<ComponenteOperando> Operandos { get; set; }
        public override double ObtenerValor(Empresa empresa, int periodo)
        {
            double result = 0;
            //Parsear la formula
            //Aplicar la formula
            return result;
        }
    }
}