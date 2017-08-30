using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TpIntegradorDiuj.Models;
using TpIntegradorDiuj.Models.Condiciones;

namespace TpIntegradorDiuj.Controllers
{
    public class CondicionesController : Controller
    {
        TpIntegradorDbContext db = TpIntegradorDbContext.GetInstance();
        // GET: Condiciones
        public ActionResult Index()
        {
            List<Condicion> condiciones = db.Condiciones.Include("Indicador").ToList();
            return View(condiciones);
        }
        private void setViewbag()
        {
            ViewBag.ListIndicadores = db.Indicadores.Select(x => new SelectListItem
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
                db.Condiciones.Add(condicion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);
                setViewbag();
                return View();
            }
        }
    }
}