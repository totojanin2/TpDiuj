using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TpIntegradorDiuj.Models.Condiciones
{
    public class CondicionesFactory
    {
        public static Condicion CreateCondicion(CondicionModel model)
        {
            Condicion condi;
            switch (model.Tipo)
            {
                case TipoCondicion.Creciente:
                    condi = new Creciente();
                break;
                case TipoCondicion.MayorAUno:
                    condi = new MayorAUno();
                break;
                case TipoCondicion.RoeConsistente:
                    condi = new RoeConsistente();
                break;
                default:
                    condi = new MayorAUno();
                break;
            }
            condi.Tipo = model.Tipo;
            condi.Indicador_Id = model.Indicador_Id;
            condi.Descripcion = model.Descripcion;
            return condi;
        }
    }
}