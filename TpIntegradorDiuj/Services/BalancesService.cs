using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TpIntegradorDiuj.Models;

namespace TpIntegradorDiuj.Services
{
    public class BalancesService
    {
        public static List<Balance> GetAll()
        {
            TpIntegradorDbContext db = new TpIntegradorDbContext();
            return db.Balances.ToList();
        }
    }
}