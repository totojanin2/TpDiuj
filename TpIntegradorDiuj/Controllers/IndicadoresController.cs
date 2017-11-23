
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TpIntegradorDiuj.Models;
using TpIntegradorDiuj.Services;

namespace TpIntegradorDiuj.Controllers
{
    public class IndicadoresController : Controller
    {
        // GET: Indicadores
        EmpresasController empController = new EmpresasController();
        TpIntegradorDbContext db = TpIntegradorDbContext.GetInstance();
        public ActionResult Index()
        {
            var store = new UserStore<ApplicationUser>(new TpIntegradorDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            List<Indicador> indicadores = user.Indicadores;
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
        public ActionResult Edit(int idInd)
        {
            Indicador indAMod = IndicadoresService.GetById(idInd);
            return View(indAMod);
        }
        [HttpPost]
        public ActionResult Edit(Indicador model)
        {
            IndicadoresService.Editar(model);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int idInd)
        {
            IndicadoresService.Eliminar(idInd);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ObtenerFormulaDelIndicador(int idIndicador)
        {
            Indicador indicador = IndicadoresService.GetById(idIndicador);
            return Json(new { Formula = indicador.Formula });
        }
        [HttpPost]
        public ActionResult Create(Indicador model)
        {
            try
            {
                IndicadoresService.Crear(model, this.User.Identity.GetUserId());              
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(model);
            }
           
        }
        public ActionResult EvaluarIndicadorParaEmpresa(int idIndicador,string cuit,int periodo)
        {
            try
            {
                var store = new UserStore<ApplicationUser>(new TpIntegradorDbContext());
                var userManager = new UserManager<ApplicationUser>(store);
                ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
                List<Indicador> indicadoresDelUsuario = user.Indicadores;
                double valorTrasAplicarIndicador = IndicadoresService.EvaluarIndicadorParaEmpresa(idIndicador, cuit, periodo, indicadoresDelUsuario);       
                return Json(new { Success = true, Valor = valorTrasAplicarIndicador });
            }
            catch(Exception e)
            {
                return Json(new { Success = false, Error = "Error: verificar que haya operandos para el año seleccionado." });
            }
        }

    }
}