using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TpIntegradorDiuj.Models;

namespace TpIntegradorDiuj.Controllers
{
    public class BalancesController : Controller
    {
        // GET: Cuentas
        EmpresasController empController = new EmpresasController();
        TpIntegradorDbContext db = TpIntegradorDbContext.GetInstance();

        public ActionResult Index()
        {
            List<Balance> balances = db.Balances.ToList();
            return View(balances);
        }
        public ActionResult Create()
        {
            ViewBag.Empresas = db.Empresas.Select(x => new SelectListItem
            {
                Text = x.Nombre,
                Value = x.Id.ToString()
            }).ToList();
            return View();
        }
        public ActionResult Details(int id)
        {
            Balance bal = db.Balances.FirstOrDefault(x => x.Id == id);
            return View(bal);
        }
        private void setViewBagEmpresa()
        {
            ViewBag.Empresas = db.Empresas.Select(x => new SelectListItem
            {
                Text = x.Nombre,
                Value = x.Id.ToString()
            }).ToList();
        }
        [HttpPost]
        public ActionResult Create(Balance balanceModel)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Hay errores en el formulario");
                setViewBagEmpresa();
                return View();
            }
            bool hayBalancesIguales = db.Balances.Any(x => x.Periodo == balanceModel.Periodo && x.Empresa_Id == balanceModel.Empresa_Id);
            if(hayBalancesIguales)
            {
                ModelState.AddModelError("", "Ya existe un balance para esa empresa en ese período.");
                setViewBagEmpresa();
                return View();
            }
            try
            {
                db.Balances.Add(balanceModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
               ModelState.AddModelError("", e.Message);
               setViewBagEmpresa();
               return View();
            }

        }
        [HttpPost]
        public JsonResult obtener_periodos_empresa(int idEmpresa)
        {
            try
            {
                List<int> periodos = db.Balances.Where(x => x.Empresa_Id == idEmpresa).Select(x => x.Periodo).ToList();
                return Json(new { Success = true, Periodos = periodos });

            }
            catch (Exception e)
            {
                return Json(new { Success = false, Mensaje = "Hubo un error" });

            }
         
        }
    }
}