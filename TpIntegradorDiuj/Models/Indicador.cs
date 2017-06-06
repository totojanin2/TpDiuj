using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TpIntegradorDiuj.Models
{
    public class Indicador
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Formula { get; set; }
        public int Empresa_Id { get; set; }
    }
}