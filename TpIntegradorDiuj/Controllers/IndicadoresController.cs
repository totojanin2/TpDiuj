
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TpIntegradorDiuj.Models;

namespace TpIntegradorDiuj.Controllers
{
    public class IndicadoresController : Controller
    {
        // GET: Indicadores
        EmpresasController empController = new EmpresasController();
        public ActionResult Index()
        {
            List<Indicador> indicadores = DeserializarArchivoIndicadores();            
            return View(indicadores);
        }
        public List<Indicador> DeserializarArchivoIndicadores()
        {
            string buf = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/Archivos/") + "indicadores.json");
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Indicador> listIndicadores = serializer.Deserialize<List<Indicador>>(buf);
            return listIndicadores;

        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ObtenerFormulaDelIndicador(int idIndicador)
        {
            var indicador = DeserializarArchivoIndicadores().FirstOrDefault(x => x.Id == idIndicador);
            return Json(new { Formula = indicador.Formula });
        }
        [HttpPost]
        public ActionResult Create(Indicador model)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Indicador>  indicadores=DeserializarArchivoIndicadores();
            int maxId = indicadores.Select(x => x.Id).Max();
            model.Id = maxId+1;
            indicadores.Add(model);
            //Guardo el indicador en lel JSON
            string jsonData = JsonConvert.SerializeObject(indicadores);
            System.IO.File.WriteAllText(Server.MapPath("~/App_Data/Archivos/") + "indicadores.json", jsonData);
            return RedirectToAction("Index");
        }
        public ActionResult EvaluarIndicadorParaEmpresa(int idIndicador,int idEmpresa,int periodo)
        {
            var indicador = DeserializarArchivoIndicadores().FirstOrDefault(x => x.Id == idIndicador);
            var empresa = empController.DeserializarArchivoEmpresas().FirstOrDefault(x => x.Id == idEmpresa);
            //Aplico el indicador, es decir, hay que parsear la formula
            double valorTrasAplicarIndicador = indicador.ObtenerValor(empresa, periodo);
            return Json(new { Valor = valorTrasAplicarIndicador });
        }
       
    }
}