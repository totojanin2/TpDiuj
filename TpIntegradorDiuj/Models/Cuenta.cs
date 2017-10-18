using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TpIntegradorDiuj.Models
{
    public class Cuenta : ComponenteOperando
    {

        public int Balance_Id { get; set; }
        public double Valor { get; set; }
        
        public override double ObtenerValor(Empresa empresa, int periodo)
        {
            // buscar cuenta en la db y devolver el valor correspondiente
            throw new NotImplementedException();
        }
    }
}