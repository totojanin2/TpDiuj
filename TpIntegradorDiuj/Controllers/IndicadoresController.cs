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
    public class IndicadoresController : Controller
    {
        // GET: Indicadores
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
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