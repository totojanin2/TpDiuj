using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using TpIntegradorDiuj.Controllers;
using TpIntegradorDiuj.Models;

namespace TpIntegradorDiuj.Services
{
    public class BalancesService
    {
       static string directorio = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data\\Balances");

        public static List<Balance> GetAll()
        {
            TpIntegradorDbContext db = new TpIntegradorDbContext();
            return db.Balances.ToList();
        }
        public static Balance GetById(int id)
        {
            TpIntegradorDbContext db = new TpIntegradorDbContext();
            Balance bal = db.Balances.FirstOrDefault(x => x.Id == id);
            ////////////////////////////////////////////////////////////
            //bal.Empresa = EmpresasService.GetByCUIT(bal.Empresa_CUIT);
            ////////////////////////////////////////////////////////////
            return bal;
        }
        public static void Editar(Balance balanceEditado)
        {
            TpIntegradorDbContext db = new TpIntegradorDbContext();
            Balance balanceAEdit = BalancesService.GetBalanceByPeriodoYEmpresa(balanceEditado.Periodo, balanceEditado.Empresa_CUIT);
            IQueryable<Cuenta> cuentasAEliminar = db.Cuentas.Where(x => x.Balance_Id == balanceAEdit.Id);
            db.Cuentas.RemoveRange(cuentasAEliminar);
            db.SaveChanges();
            List<Cuenta> cuentasNuevas = balanceEditado.Cuentas;
            foreach (var item in cuentasNuevas)
            {
                item.Balance_Id = balanceAEdit.Id;
            }          
            db.Cuentas.AddRange(cuentasNuevas);

            db.SaveChanges();
        }
        public static bool ExisteBalanceParaEmpresaEnPeriodo(int periodo,string empresaCuit)
        {
            TpIntegradorDbContext db = new TpIntegradorDbContext();
            return db.Balances.Any(x => x.Periodo == periodo && x.Empresa_CUIT == empresaCuit);
        }
        public static Balance GetBalanceByPeriodoYEmpresa(int periodo, string empresaCuit)
        {
            TpIntegradorDbContext db = new TpIntegradorDbContext();
            return db.Balances.FirstOrDefault(x => x.Periodo == periodo && x.Empresa_CUIT == empresaCuit);
        }

        public static void CargarBalances(List<Balance> balances)
        {
            TpIntegradorDbContext db = new TpIntegradorDbContext();
            //Valido que no haya balances repetidos
            BalancesService.ValidarBalancesArchivo(balances);
            db.Balances.AddRange(balances);
            db.SaveChanges();
        }
        public static void Crear(Balance bal)
        {
            TpIntegradorDbContext db = new TpIntegradorDbContext();
            db.Balances.Add(bal);
            db.SaveChanges();
        }
        public static void Eliminar(int id)
        {
            TpIntegradorDbContext db = new TpIntegradorDbContext();
            Balance balanceAEliminar = db.Balances.FirstOrDefault(x => x.Id == id);
            db.Balances.Remove(balanceAEliminar);
            db.SaveChanges();
        }
        private static void ValidarBalancesArchivo(List<Balance> balancesArchivo)
        {
            List<Balance> balancesRepetidos = new List<Balance>();
            foreach (var item in balancesArchivo)
            {
                bool hayUnBalanceIgual = BalancesService.ExisteBalanceParaEmpresaEnPeriodo(item.Periodo, item.Empresa_CUIT);
                if (hayUnBalanceIgual)
                {
                    balancesRepetidos.Add(item);
                }

            }
            if (balancesRepetidos.Count > 0)
            {
                throw new BalancesRepetidosException("Hay balances del archivo que ya fueron cargados previamente") { Balances = balancesRepetidos };
            }
        }
        public static void ProcesarArchivo(string nameFile)
        {
            string buffer = System.IO.File.ReadAllText(nameFile);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Balance> listBalances = serializer.Deserialize<List<Balance>>(buffer);
            //Obtengo los balances del archivo en procesamiento
            foreach (var balance in listBalances)
            {
                bool hayUnBalanceIgual = BalancesService.ExisteBalanceParaEmpresaEnPeriodo(balance.Periodo, balance.Empresa_CUIT);
                if (hayUnBalanceIgual)
                {
                    //Modifico el balance
                    BalancesService.Editar(balance);
                }
                else
                {
                    //Agrego el balance
                    BalancesService.Crear(balance);
                }
            }
        }
        public static void BatchArchivosBalances()
        {
            TpIntegradorDbContext db = new TpIntegradorDbContext();
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    //Obtener archivos en la carpeta del directorio predeterminada
                    string[] namesFiles = Directory.GetFiles(directorio);
                    foreach (var nameFile in namesFiles)
                    {
                     //Por cada archivo obtenido, procesarlo:
                        //      Agregar los balances que no existen y modificar los balances existentes
                        BalancesService.ProcesarArchivo(nameFile);
                        //      Eliminar el archivo procesado del directorio
                        File.Delete(nameFile);
                    }
                    tx.Commit();
                }
                catch(Exception e)
                {
                    tx.Rollback();
                }

            }
           
        }
        public static List<int> GetPeriodosDeBalancesDeEmpresa(string cuit)
        {
            TpIntegradorDbContext db = new TpIntegradorDbContext();
            return db.Balances.Where(x => x.Empresa_CUIT == cuit).Select(x => x.Periodo).ToList();
        }
    }
}