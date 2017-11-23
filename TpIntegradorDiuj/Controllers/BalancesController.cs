using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TpIntegradorDiuj.Models;
using TpIntegradorDiuj.Services;

namespace TpIntegradorDiuj.Controllers
{
    public class BalancesController : Controller
    {
        // GET: Cuentas
        EmpresasController empController = new EmpresasController();
        TpIntegradorDbContext db = TpIntegradorDbContext.GetInstance();

        public ActionResult Index()
        {
            List<Balance> balances = BalancesService.GetAll();
            foreach (var item in balances)
            {
                item.Empresa = EmpresasService.GetById(item.Empresa_Id);
            }
            return View(balances);
        }
        public List<Balance> DeserializarArchivoBalances()
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
                buffer = System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data\\Archivos\\balances.json"));
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Balance> listBalances = serializer.Deserialize<List<Balance>>(buffer);
            return listBalances;

        }
        [HttpPost]
        public ActionResult CargarBalancesDesdeArchivo()
        {
            try
            {
                if (Request.Files.Count > 0)
                {
                    //Deserializo el archivo seleccionado
                    List<Balance> balancesArchivo = this.DeserializarArchivoBalances();
                    BalancesService.CargarBalances(balancesArchivo);                  
                    return Json(new { Success = true });
                }
                else
                {
                    throw new Exception("Ingrese un archivo de balances");
                }
            }
            catch (BalancesRepetidosException bre)
            {
                return Json(new { Success = false, Error = bre.Message,Balances = bre.Balances });

            }
            catch (Exception e)
            {
                return Json(new { Success = false, Error = e.Message });
            }
        }

  

        [HttpPost]
        public ActionResult Create(Balance balanceModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Hay errores en el formulario");
                setViewBagEmpresa();
                return View();
            }
           
            try
            {
                if (balanceModel.Cuentas.Count == 0)
                    throw new Exception("Debe ingresar por lo menos una cuenta para este balance");
                //Busco en la base de datos si hay algun balance con ese periodo para esa empresa
                bool hayBalancesIguales = BalancesService.ExisteBalanceParaEmpresaEnPeriodo(balanceModel.Periodo, balanceModel.Empresa_Id);
                if (hayBalancesIguales)
                {
                    ModelState.AddModelError("", "Ya existe un balance para esa empresa en ese período.");
                    setViewBagEmpresa();
                    return View();
                }
                BalancesService.Crear(balanceModel);              
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                setViewBagEmpresa();
                return View();
            }

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
                if (model.Cuentas.Count == 0)
                    throw new Exception("Debe ingresar por lo menos una cuenta para este balance");
                BalancesService.Editar(model);
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);
                setViewBagEmpresa();
                Balance balance = BalancesService.GetById(model.Id);
                return View(balance);
            }
           
        }
        public ActionResult Edit(int id)
        {
            setViewBagEmpresa();
            Balance balance = BalancesService.GetById(id);
            return View(balance);
        }
        public ActionResult Details(int id)
        {
            Balance bal = BalancesService.GetById(id);
            return View(bal);
        }
        private void setViewBagEmpresa()
        {
            ViewBag.Empresas = EmpresasService.GetAll().Select(x => new SelectListItem
            {
                Text = x.Nombre,
                Value = x.Id.ToString()
            }).ToList();
        }
        
        [HttpPost]
        public JsonResult ObtenerBalancesDeEmpresaPorPeriodo(int idEmpresa, int anio)
        {
            try
            {
                Empresa empresa = EmpresasService.GetById(idEmpresa);
                //Obtengo el balance de la empresa para el año solicitado
                Balance balance = BalancesService.GetBalanceByPeriodoYEmpresa(anio, idEmpresa);
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
                List<int> periodos = BalancesService.GetPeriodosDeBalancesDeEmpresa(idEmpresa);
                return Json(new { Success = true, Periodos = periodos });

            }
            catch (Exception e)
            {
                return Json(new { Success = false, Mensaje = "Hubo un error" });

            }
         
        }
    }
}