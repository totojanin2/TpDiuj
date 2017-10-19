using System.Collections.Generic;

namespace TpIntegradorDiuj.Models
{
    public abstract class ComponenteOperando
    {
        public int Id { get; set; }
        public int? IndicadorPadre_Id { get; set; }
        public string Nombre { get; set; }
        public abstract double ObtenerValor(Empresa empresa, int periodo, List<ComponenteOperando> listaOperandos);
    }
}