using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TpIntegradorDiuj.Models
{
    public class Balance
    {
        public int Id { get; set; }
        public virtual List<Cuenta> Cuentas { get; set; }
        public int Periodo { get; set; }
        public int Empresa_Id { get; set; }


        [NotMapped]
        public  Empresa Empresa { get; set; }

        public double Total {
            get {
                return this.Cuentas.Sum(x => x.Valor);
            }
        }




    }
}