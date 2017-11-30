using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TpIntegradorDiuj.Models;
using TpIntegradorDiuj.Models.Condiciones;
using TpIntegradorDiuj.Services;

namespace TpIntegradorDiuj.Controllers
{
    public class CondicionesController : Controller
    {

        TpIntegradorDbContext db;
        CondicionesService condicionesService;
        IndicadoresService indService;

        public CondicionesController()
        {
            db = TpIntegradorDbContext.GetInstance();
            condicionesService = new CondicionesService(db);
            indService = new IndicadoresService(db);
        }

        // GET: Condiciones
        public ActionResult Index()
        {
            List<Condicion> condiciones = condicionesService.GetAll();
            return View(condiciones);
        }
        private void setViewbag()
        {
            ViewBag.ListIndicadores = indService.GetAll().Select(x => new SelectListItem
            {
                Text = x.Nombre,
                Value = x.Id.ToString()
            }).ToList();
        }
        public ActionResult Create()
        {
            setViewbag();
            return View();
        }
        [HttpPost]
        public ActionResult Create(CondicionModel model)
        {
            try
            {
                Condicion condicion = CondicionesFactory.CreateCondicion(model);
                condicionesService.Crear(condicion);
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);
                setViewbag();
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            condicionesService.Eliminar(id);
            return RedirectToAction("Index");
        }

    }
}