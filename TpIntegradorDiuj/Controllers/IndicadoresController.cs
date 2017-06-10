

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
        public ActionResult Index()
        {
            ViewBag.ListIndicadores = DeserializarArchivoIndicadores().Select(x => new SelectListItem {
                Text = x.Nombre,
                Value = x.Id.ToString()
            }).ToList();
            
            return View();
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
            var indicadorAAgregar = serializer.Serialize(model);
            //StreamReader file = File.OpenText(@"c:\videogames.json");
            return RedirectToAction("Index");
        }
    }
}