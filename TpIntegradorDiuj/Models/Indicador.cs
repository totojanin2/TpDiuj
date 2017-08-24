using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace TpIntegradorDiuj.Models
{
    public class Indicador : ComponenteOperando
    {
        const string pattern = @"((\b([A-z]*[0-9]*|[0-9]*[A-z]*)[a-z0-9]*\b)([+\-\*\/]\(?(\b([a-z]*[0-9]*|[0-9]*[a-z]*)[a-z0-9]*\b)\)?)+)";
        public string Formula { get; set; }
        public List<ComponenteOperando> Operandos { get; set; }
        public override double ObtenerValor(Empresa empresa, int periodo)
        {
            double result = 0;
            //Parsear la formula
            //Aplicar la formula
            return result;
        }
        public bool ValidarExpresionFormula()
        {
            string str = this.Formula.Trim().ToLower();
            return Regex.IsMatch(str, pattern);
        }
    }
}