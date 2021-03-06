﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TpIntegradorDiuj.Models
{
    public class MayorAUno : Condicion
    {
        public MayorAUno()
        {
        }

        public MayorAUno(string descripcion,int indicador_id): base(descripcion,indicador_id)
        {

        }
        public override bool Analizar(Empresa empresa, List<ComponenteOperando> lista)
        {
            bool result = true;
            List<int> periodos = this.ObtenerPeriodosAConsultar(empresa);
            int i = 0;
            while (i < periodos.Count && result)
            {
                result = this.Indicador.ObtenerValor(empresa, i,lista)>1;
                i++;
            }
            return result;
        }
    }
}