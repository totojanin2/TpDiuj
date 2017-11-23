using Microsoft.ApplicationInsights.Extensibility.Implementation;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TpIntegradorDiuj.Models;
using TpIntegradorDiuj.Services;

namespace TpIntegradorDiuj.Controllers
{
    public class EmpresasController : Controller
    {
        // GET: Empresas
        TpIntegradorDbContext db = TpIntegradorDbContext.GetInstance();
        public ActionResult Index()
        {
            List<Empresa> empresas = EmpresasService.GetAll();
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
        public ActionResult TraerEmpresas()
        {
            List<Empresa> empresas = EmpresasService.GetAll();
            return Json(new { Empresas = empresas},JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(string cuit)
        {
            Empresa empresaAModificar = EmpresasService.GetByCUIT(cuit);
            return View(empresaAModificar);
        }
        [HttpPost]
        public ActionResult Edit(Empresa model)
        {
            EmpresasService.Editar(model);          
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string cuit)
        {
            EmpresasService.Eliminar(cuit);         
            return RedirectToAction("Index");
        } 
    
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Empresa emp)
        {
            EmpresasService.Crear(emp);
            return RedirectToAction("Index");
        }
        
   
    }
}