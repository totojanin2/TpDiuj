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
            foreach (var item in balances)
            {
                item.Empresa = db.Empresas.FirstOrDefault(x => x.Id == item.Empresa_Id);
            }
            return View(balances);
        }
        public ActionResult Create()
        {
            setViewBagEmpresa();
            return View();
        }
        [HttpPost]
        public ActionResult Edit(Balance model)
        {
            try
            {
                Balance balanceAEdit = db.Balances.FirstOrDefault(x => x.Periodo == model.Periodo && x.Empresa_Id == model.Empresa_Id);
                balanceAEdit.Cuentas.AddRange(model.Cuentas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);
                setViewBagEmpresa();
                return View();
            }
           
        }
        public ActionResult Edit()
        {
            setViewBagEmpresa();
            return View();
        }
        public ActionResult Details(int id)
        {
            Balance bal = db.Balances.FirstOrDefault(x => x.Id == id);
            bal.Empresa = db.Empresas.First(x => x.Id == bal.Empresa_Id);
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
        public JsonResult ObtenerBalancesDeEmpresaPorPeriodo(int idEmpresa, int anio)
        {
            try
            {
                List<Empresa> empresas = db.Empresas.ToList();
                Empresa empresa = empresas.FirstOrDefault(x => x.Id == idEmpresa);
                //Obtengo el balance de la empresa para el año solicitado
                Balance balance = db.Balances.FirstOrDefault(x => x.Empresa_Id == idEmpresa && x.Periodo == anio);
                if (balance != null)
                {
                    return Json(new { Success = true, Cuentas = balance.Cuentas });
                }
                else
                {
                    return Json(new { Success = false, Mensaje = "No hay cuentas para: " + empresa.Nombre + " en el periodo " + anio });

                }
            }
            catch(Exception e)
            {
                
                return Json(new { Success = false, Mensaje = e.Message});

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