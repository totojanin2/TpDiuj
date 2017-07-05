using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TpIntegradorDiuj.Models
{
    public class Balance
    {
        public int Id { get; set; }
        public virtual Cuenta Cuenta { get; set; }
        public int Cuenta_Id { get; set; }
        public int Periodo { get; set; }
        public int Empresa_Id { get; set; }

        public double Valor { get; set; }




    }
}