﻿using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
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
        TpIntegradorDbContext db;
        BalancesService balanceService;
        EmpresasService empService;
        public BalancesController()
        {
            db = TpIntegradorDbContext.GetInstance();
            balanceService = new BalancesService(db);
            empService = new EmpresasService(db);
        }

        public ActionResult Index()
        {
            List<Balance> balances = balanceService.GetAll();
            foreach (var item in balances)
            {
                item.Empresa = empService.GetByCUIT(item.Empresa_CUIT);
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
                    balanceService.CargarBalances(balancesArchivo);                  
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
        public ActionResult Delete(int id)
        {
            balanceService.Eliminar(id);
            return RedirectToAction("Index");
        }
  

        [HttpPost]
        public ActionResult Create(Balance balanceModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Hay errores en el formulario, los valores erroneos se predeterminaran como 0");
                setViewBagEmpresa();
                return View(balanceModel);
            }
           
            try
            {
                if (balanceModel.Cuentas.Count == 0)
                    throw new Exception("Debe ingresar por lo menos una cuenta para este balance");
                //Busco en la base de datos si hay algun balance con ese periodo para esa empresa
                bool hayBalancesIguales = balanceService.ExisteBalanceParaEmpresaEnPeriodo(balanceModel.Periodo, balanceModel.Empresa_CUIT);
                if (hayBalancesIguales)
                {
                    ModelState.AddModelError("", "Ya existe un balance para esa empresa en ese período.");
                    setViewBagEmpresa();
                    return View(balanceModel);
                }
                balanceService.Crear(balanceModel);              
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                setViewBagEmpresa();
                return View(balanceModel);
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
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Hay errores en el formulario, los valores erroneos se predeterminaran como 0");
                setViewBagEmpresa();
                return View(model);
            }
            try
            {
                if (model.Cuentas.Count == 0)
                    throw new Exception("Debe ingresar por lo menos una cuenta para este balance");
                balanceService.Editar(model);
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);
                setViewBagEmpresa();
                return View(model);
            }
           
        }
        public ActionResult Edit(int id)
        {
            setViewBagEmpresa();
            Balance balance = balanceService.GetById(id);
            return View(balance);
        }
        public ActionResult Details(int id)
        {
            Balance bal = balanceService.GetById(id);
            
            return View(bal);
        }

        public ActionResult UploadBlob()
        {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting("diujstorage_AzureStorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("contenedor");
            CloudBlockBlob blob = container.GetBlockBlobReference("archivo");
            using (var fileStream = System.IO.File.OpenRead("C:/Git/TpDiuj/TpIntegradorDiuj/App_Data/Balances/2016.json"))
            {
                blob.UploadFromStream(fileStream);
            }

            return View();
        }

        private void setViewBagEmpresa()
        {
            ViewBag.Empresas = empService.GetAll().Select(x => new SelectListItem
            
            {
                Text = x.Nombre,
                Value = x.CUIT
            }).ToList();
            
        }
        
        [HttpPost]
        public JsonResult ObtenerBalancesDeEmpresaPorPeriodo(string cuitEmpresa, int anio)
        {
            try
            {
                Empresa empresa=new Empresa();                
                empresa = empService.GetByCUIT(cuitEmpresa);
                //Obtengo el balance de la empresa para el año solicitado
                Balance balance = balanceService.GetBalanceByPeriodoYEmpresa(anio, cuitEmpresa);
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
        public JsonResult obtener_periodos_empresa(string cuit)
        {
            try
            {
                List<int> periodos = balanceService.GetPeriodosDeBalancesDeEmpresa(cuit);
                return Json(new { Success = true, Periodos = periodos });

            }
            catch (Exception e)
            {
                return Json(new { Success = false, Mensaje = "Hubo un error" });

            }
         
        }
    }
}