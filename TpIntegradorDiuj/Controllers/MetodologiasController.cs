using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TpIntegradorDiuj.Models;
using TpIntegradorDiuj.Services;

namespace TpIntegradorDiuj.Controllers
{
    public class MetodologiasController : Controller
    {
        // GET: Metodologias
        TpIntegradorDbContext db;
        MetodologiasService metService;
        CondicionesService condService;
        EmpresasService empService;
         public MetodologiasController()
        {
            db = TpIntegradorDbContext.GetInstance();
            condService = new CondicionesService(db);
            metService = new MetodologiasService(db);
            empService = new EmpresasService(db);
        }
        public ActionResult Index()
        {
            List<Metodologia> metodologias = metService.GetAll();
            return View(metodologias);
        }
        private void setViewBagCondiciones()
        {
            ViewBag.ListCondiciones = condService.GetAll().Select(x => new SelectListItem
            {
                Text = x.Descripcion,
                Value = x.Id.ToString()
            }).ToList();
        }
        public ActionResult Create()
        {
            setViewBagCondiciones();
            return View();
        }
      
        [HttpPost]
        public ActionResult Create(Metodologia model,List<int> IdsCondiciones)
        {
            try
            {
                metService.Agregar(model, IdsCondiciones);
                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                setViewBagCondiciones();
                return View();
            }
        }
        
        public List<Metodologia> DeserializarArchivoMetodologias()
        {
            //TODO: Falta crear el archivo json de las metodologias
            string buf = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/Archivos/") + "metodologias.json");
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Metodologia> listMetodologias = serializer.Deserialize<List<Metodologia>>(buf);
            return listMetodologias;

        }
        public ActionResult ObtenerEmpresasDeseables(int idMetodologia)
        {
            Metodologia met = metService.GetById(idMetodologia);
            List<Empresa> empresas = empService.GetAll();
            List<Empresa> deseables = met.ObtenerEmpresasDeseables(empresas,db.Operandos.ToList());
            ViewBag.Metodologia_Nombre = met.Nombre;
            return View(deseables);
        }
        public ActionResult EvaluarConvenienciaInversion(string empresaCuit,int metodologiaId)
        {            
            bool result = metService.EvaluarConvenienciaInversion(empresaCuit, metodologiaId);
            return Json(new { EsDeseable = result });
        }


        public ActionResult Delete(int id)
        {
            metService.Eliminar(id);
            return RedirectToAction("Index");
        }
    }
}