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
        public List<Empresa> DeserializarArchivoEmpresas()
        {
            var file = Request.Files[0];
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var buffer = new StreamReader(file.InputStream).ReadToEnd();
            List<Empresa> listaEmpresas =  serializer.Deserialize<List<Empresa>>(buffer);
            return listaEmpresas;

        }
        [HttpPost]
        public JsonResult ObtenerEmpresasYPeriodos()
        {
            if (Request.Files.Count>0)
            {               
                List<Empresa> empresas = this.DeserializarArchivoEmpresas();
                List<int> periodos = new List<int>();
                foreach (var balances in empresas.Select(x=>x.Balances))
                {
                    foreach (var item in balances)                    
                        periodos.Add(item.Periodo);                   
                }
                periodos = periodos.Distinct().ToList();
                return Json(new { Success = true, Empresas = empresas, Periodos = periodos });
            }
            return Json(new { Success = false, Mensaje = "Hubo un error" });
        }
        [HttpPost]
        public JsonResult ObtenerBalancesDeEmpresaPorPeriodo(int idEmpresa,int anio)
        {
            if (Request.Files.Count > 0)
            {                
                List<Empresa> empresas = this.DeserializarArchivoEmpresas();
                Empresa empresa = empresas.FirstOrDefault(x => x.Id == idEmpresa);
                var balances = empresa.Balances.Where(x => x.Periodo == anio);
                if (balances != null)
                {
                    return Json(new { Success = true, Balances = balances });
                }
                else
                {
                   return Json(new { Success = false, Mensaje="No hay cuentas para: "+empresa.Nombre + " en el periodo "+anio });

                }
            }
            return Json(new { Success = false, Mensaje = "Hubo un error" });
        }
   
    }
}