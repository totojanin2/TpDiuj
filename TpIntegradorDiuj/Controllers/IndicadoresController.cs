
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TpIntegradorDiuj.Models;

namespace TpIntegradorDiuj.Controllers
{
    public class IndicadoresController : Controller
    {
        // GET: Indicadores
        EmpresasController empController = new EmpresasController();
        TpIntegradorDbContext db = TpIntegradorDbContext.GetInstance();
        public ActionResult Index()
        {
            List<Indicador> indicadores = db.Indicadores.ToList();            
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
            var indicador = db.Indicadores.FirstOrDefault(x => x.Id == idIndicador);
            return Json(new { Formula = indicador.Formula });
        }
        [HttpPost]
        public ActionResult Create(Indicador model)
        {
            try
            {
                
                model.ValidarExpresionFormula(db.Indicadores);               
                db.Indicadores.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(model);
            }
           
        }
        public ActionResult EvaluarIndicadorParaEmpresa(int idIndicador,int idEmpresa,int periodo)
        {
           //Obtengo el indicador y empresa solicitada
            Indicador indicador = db.Indicadores.FirstOrDefault(x => x.Id == idIndicador);
            Empresa empresa = db.Empresas.FirstOrDefault(x => x.Id == idEmpresa);
            //Aplico el indicador, es decir, hay que parsear la formula
            double valorTrasAplicarIndicador = indicador.ObtenerValor(empresa, periodo);
            return Json(new { Valor = valorTrasAplicarIndicador });
        }
       
    }
}