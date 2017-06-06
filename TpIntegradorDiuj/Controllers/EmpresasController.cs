using Microsoft.ApplicationInsights.Extensibility.Implementation;
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
    public class EmpresasController : Controller
    {
        // GET: Empresas
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ObtenerEmpresas(HttpPostedFileBase f)
        {
            // Verify that the user selected a file
            if (f != null && f.ContentLength > 0)
            {   JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<Empresa> empresas = serializer.Deserialize<List<Empresa>>(f.ToString());
                return Json(new { Success=true,Empresas = empresas.ToList() });
            }
            return Json(new { Success = false, Mensaje = "Hubo un error" });
        }
    }
}