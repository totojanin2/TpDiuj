namespace TpIntegradorDiuj.Models
{
    public abstract class ComponenteOperando
    {
        public string Nombre { get; set; }
        public abstract double ObtenerValor(Empresa empresa, int periodo);
    }
}