using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TpIntegradorDiuj.Models
{
    public class Cuenta
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Valor { get; set; }
        public int Balance_Id { get; set; }
    }
}