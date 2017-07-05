using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TpIntegradorDiuj.Models;

namespace TpIntegradorDiuj.Controllers
{
    public class MetodologiasController : Controller
    {
        // GET: Metodologias
        public ActionResult Index()
        {
            var metodologias = DeserializarArchivoMetodologias();
            return View(metodologias);
        }
        public ActionResult Create()
        {
            return View();
        }
      
        [HttpPost]
        public ActionResult Create(Metodologia model)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Metodologia> metodologias = DeserializarArchivoMetodologias();
            int maxId = metodologias.Select(x => x.Id).Max();
            model.Id = maxId + 1;
            metodologias.Add(model);
            string jsonData = JsonConvert.SerializeObject(metodologias);
            System.IO.File.WriteAllText(Server.MapPath("~/App_Data/Archivos/") + "metodologias.json", jsonData);
            return RedirectToAction("Index");
        }
        
        public List<Metodologia> DeserializarArchivoMetodologias()
        {
            string buf = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/Archivos/") + "metodologias.json");
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Metodologia> listMetodologias = serializer.Deserialize<List<Metodologia>>(buf);
            return listMetodologias;

        }

        public ActionResult EvaluarConvenienciaInversion(int empresaId,int metodologiaId)
        {
            EmpresasController empController = new EmpresasController();
            var empresa = empController.DeserializarArchivoEmpresas().FirstOrDefault(x=>x.Id == empresaId);
            var metodologia = DeserializarArchivoMetodologias().FirstOrDefault(x => x.Id == metodologiaId);
            bool result = metodologia.EsDeseableInvertir(empresa);
            return Json(new { EsDeseable = result });
        }
    }
}