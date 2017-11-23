using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TpIntegradorDiuj.Models;

namespace TpIntegradorDiuj.Services
{
    public class IndicadoresService
    {
        public static Indicador GetById(int id)
        {
            TpIntegradorDbContext db = new TpIntegradorDbContext();
            return db.Indicadores.FirstOrDefault(x => x.Id == id);
        }
        public static List<Indicador> GetAll()
        {
            TpIntegradorDbContext db = new TpIntegradorDbContext();
            return db.Indicadores.ToList();
        }
        public static void Editar(Indicador indicadorEditado)
        {
            TpIntegradorDbContext db = new TpIntegradorDbContext();
            Indicador indOriginal = IndicadoresService.GetById(indicadorEditado.Id);
            indOriginal.Editar(indicadorEditado);
            db.SaveChanges();
        }
        public static void Crear(Indicador indicador,string usuarioID)
        {
            TpIntegradorDbContext db = new TpIntegradorDbContext();
            //Obtengo el id del usuario que esta usando el sistema y creó el indicador
            indicador.UsuarioCreador_Id = usuarioID;
            db.Indicadores.Add(indicador);
            db.SaveChanges();

        }
        public static void Eliminar(int id)
        {
            TpIntegradorDbContext db = new TpIntegradorDbContext();
            Indicador indAEliminar = IndicadoresService.GetById(id);
            db.Indicadores.Remove(indAEliminar);
            db.SaveChanges();
        }

        public static double EvaluarIndicadorParaEmpresa(int idIndicador, string cuit, int periodo, List<Indicador> indicadoresDelUsuario)
        {
            TpIntegradorDbContext db = new TpIntegradorDbContext();
            //Obtengo el indicador y empresa solicitada
            Indicador indicador = IndicadoresService.GetById(idIndicador);
            Empresa empresa = EmpresasService.GetByCUIT(cuit);
            //Aplico el indicador, es decir, hay que parsear la formula
            List<ComponenteOperando> listaOperandos = new List<ComponenteOperando>();
            listaOperandos.AddRange(db.Operandos.OfType<Cuenta>());
            listaOperandos.AddRange(indicadoresDelUsuario);
            double valorTrasAplicarIndicador = indicador.ObtenerValor(empresa, periodo, listaOperandos);
            return valorTrasAplicarIndicador;
        }
    }
}