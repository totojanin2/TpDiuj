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
        TpIntegradorDbContext db;
        public BalancesService(TpIntegradorDbContext db)
        {
            this.db = db;
        }
       static string directorio = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data\\Balances");

        public List<Balance> GetAll()
        {
            return db.Balances.ToList();
        }
        public Balance GetById(int id)
        {
            Balance bal = db.Balances.FirstOrDefault(x => x.Id == id);
            bal.Empresa = EmpresasService.GetByCUIT(bal.Empresa_CUIT);
            return bal;
        }
        public void Editar(Balance balanceEditado)
        {
            Balance balanceAEdit = this.GetBalanceByPeriodoYEmpresa(balanceEditado.Periodo, balanceEditado.Empresa_CUIT);
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
        public bool ExisteBalanceParaEmpresaEnPeriodo(int periodo,string empresaCuit)
        {
            return db.Balances.Any(x => x.Periodo == periodo && x.Empresa_CUIT == empresaCuit);
        }
        public Balance GetBalanceByPeriodoYEmpresa(int periodo, string empresaCuit)
        {
            return db.Balances.FirstOrDefault(x => x.Periodo == periodo && x.Empresa_CUIT == empresaCuit);
        }

        public void CargarBalances(List<Balance> balances)
        {
            //Valido que no haya balances repetidos
            this.ValidarBalancesArchivo(balances);
            db.Balances.AddRange(balances);
            db.SaveChanges();
        }
        public void Crear(Balance bal)
        {
            db.Balances.Add(bal);
            db.SaveChanges();
        }
        public void Eliminar(int id)
        {
            Balance balanceAEliminar = db.Balances.FirstOrDefault(x => x.Id == id);
            db.Balances.Remove(balanceAEliminar);
            db.SaveChanges();
        }
        private void ValidarBalancesArchivo(List<Balance> balancesArchivo)
        {
            List<Balance> balancesRepetidos = new List<Balance>();
            foreach (var item in balancesArchivo)
            {
                bool hayUnBalanceIgual = this.ExisteBalanceParaEmpresaEnPeriodo(item.Periodo, item.Empresa_CUIT);
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
        public void ProcesarArchivo(string nameFile)
        {
            string buffer = System.IO.File.ReadAllText(nameFile);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Balance> listBalances = serializer.Deserialize<List<Balance>>(buffer);
            //Obtengo los balances del archivo en procesamiento
            foreach (var balance in listBalances)
            {
                bool hayUnBalanceIgual = this.ExisteBalanceParaEmpresaEnPeriodo(balance.Periodo, balance.Empresa_CUIT);
                if (hayUnBalanceIgual)
                {
                    //Modifico el balance
                    this.Editar(balance);
                }
                else
                {
                    //Agrego el balance
                    this.Crear(balance);
                }
            }
        }
        public void BatchArchivosBalances()
        {
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
                        this.ProcesarArchivo(nameFile);
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
        public List<int> GetPeriodosDeBalancesDeEmpresa(string cuit)
        {
            return db.Balances.Where(x => x.Empresa_CUIT == cuit).Select(x => x.Periodo).ToList();
        }
    }
}