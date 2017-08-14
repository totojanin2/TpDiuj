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
        TpIntegradorDbContext db = TpIntegradorDbContext.GetInstance();
        public ActionResult Index()
        {
            List<Empresa> empresas = db.Empresas.ToList();
            return View(empresas);
        }
        public List<Empresa> DeserializarArchivoEmpresas()
        {
            //Metodo para desserializar el archivo json de empresas
            string buffer;
            if (Request != null)
            {
                var file = Request.Files[0];
                buffer = new StreamReader(file.InputStream).ReadToEnd();
            }
            else
            {
                buffer = System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data\\Archivos\\empresas.json"));
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Empresa> listaEmpresas =  serializer.Deserialize<List<Empresa>>(buffer);
            return listaEmpresas;

        }
       /* [HttpPost]
        public JsonResult ObtenerEmpresasYPeriodos()
        {
            if (Request.Files.Count>0)
            {               
                List<Empresa> empresas = db.Empresas.ToList();
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
        }*/
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Empresa emp)
        {
            db.Empresas.Add(emp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ObtenerBalancesDeEmpresaPorPeriodo(int idEmpresa,int anio)
        {
            if (Request.Files.Count > 0)
            {                
                List<Empresa> empresas = db.Empresas.ToList();
                Empresa empresa = empresas.FirstOrDefault(x => x.Id == idEmpresa);
                //Obtengo el balance de la empresa para el año solicitado
                Balance balance = db.Balances.FirstOrDefault(x =>x.Empresa_Id ==idEmpresa && x.Periodo == anio);                
                if(balance != null)
                {
                    return Json(new { Success = true, Cuentas = balance.Cuentas });
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