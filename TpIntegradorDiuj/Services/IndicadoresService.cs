using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TpIntegradorDiuj.Models;

namespace TpIntegradorDiuj.Services
{
    public class IndicadoresService
    {
        private TpIntegradorDbContext db;
        public IndicadoresService(TpIntegradorDbContext _db)
        {
            this.db = _db;
        }
        public Indicador GetById(int id)
        {
            return db.Indicadores.FirstOrDefault(x => x.Id == id);
        }
        public List<Indicador> GetAll()
        {
            return db.Indicadores.ToList();
        }
        public void Editar(Indicador indicadorEditado)
        {
            Indicador indOriginal = GetById(indicadorEditado.Id);
            indOriginal.Editar(indicadorEditado);
            db.SaveChanges();
        }
        public void Crear(Indicador indicador,string usuarioID)
        {
            //Obtengo el id del usuario que esta usando el sistema y creó el indicador
            indicador.UsuarioCreador_Id = usuarioID;
            db.Indicadores.Add(indicador);
            db.SaveChanges();

        }
        public void Eliminar(int id)
        {
            Indicador indAEliminar = GetById(id);
            db.Indicadores.Remove(indAEliminar);
            db.SaveChanges();
        }

        public double EvaluarIndicadorParaEmpresa(int idIndicador, string cuit, int periodo, List<Indicador> indicadoresDelUsuario)
        {
            //Obtengo el indicador y empresa solicitada
            Indicador indicador = GetById(idIndicador);
            EmpresasService empSv = new EmpresasService(db);
            Empresa empresa = empSv.GetByCUIT(cuit);
            //Aplico el indicador, es decir, hay que parsear la formula
            List<ComponenteOperando> listaOperandos = new List<ComponenteOperando>();
            listaOperandos.AddRange(db.Operandos.OfType<Cuenta>());
            listaOperandos.AddRange(indicadoresDelUsuario);
            double valorTrasAplicarIndicador = indicador.ObtenerValor(empresa, periodo, listaOperandos);
            return valorTrasAplicarIndicador;
        }
    }
}