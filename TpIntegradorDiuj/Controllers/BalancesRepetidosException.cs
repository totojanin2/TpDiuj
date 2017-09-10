using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TpIntegradorDiuj.Models;

namespace TpIntegradorDiuj.Controllers
{
    [Serializable]
    public class BalancesRepetidosException : Exception
    {
        public List<Balance> Balances { get; set; }
        public BalancesRepetidosException()
        {
        }

        public BalancesRepetidosException(string message) : base(message)
        {
        }

        public BalancesRepetidosException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BalancesRepetidosException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}