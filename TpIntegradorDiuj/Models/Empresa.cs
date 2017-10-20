using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TpIntegradorDiuj.Models
{
    public class Empresa
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public virtual List<Balance> Balances { get; set; }
        public DateTime FechaFundacion { get; set; }

        public void Editar(Empresa empresaEditada)
        {
            this.Nombre = empresaEditada.Nombre;
            this.FechaFundacion = empresaEditada.FechaFundacion;
        }
    }
}