using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using TpIntegradorDiuj.Models;
using TpIntegradorDiuj;
using System.Collections.Generic;

namespace DiujTest
{
    [TestClass]
    public class AnalizadorFormula
    {
        const string pattern = @"((\b([A-z]*[0-9]*|[0-9]*[A-z]*)[a-z0-9]*\b)([+\-\*\/]\(?(\b([a-z]*[0-9]*|[0-9]*[a-z]*)[a-z0-9]*\b)\)?)+)";
        [TestMethod]
        public void SumaSimpleEsCorrecto()
        {
            Indicador indicadorTest = new Indicador();
            indicadorTest.Formula = "cuenta + 1";
            indicadorTest.Nombre = "ind";
            Cuenta cuenta = new Cuenta();
            cuenta.Nombre = "cuenta";
            cuenta.Valor = 50;
            Indicador indicador2 = new Indicador();
            indicador2.Formula = "ind + 10";
            Empresa empresa = new Empresa();
            Balance bal = new Balance();
            bal.Empresa = empresa;
            bal.Periodo = 2017;
            bal.Cuentas.Add(cuenta);

            List<ComponenteOperando> lista = new List<ComponenteOperando>();
            lista.Add(cuenta);
            lista.Add(indicadorTest);
            int periodo = 2017;
            double result = indicador2.ObtenerValor(empresa,periodo,lista);
            Assert.AreEqual(result, 61);
        }
        [TestMethod]

        public void DivisionConParentesisEsCorrecto()
        {
            string formulaAParsear = "a/(b+c)";
            bool result = Regex.IsMatch(formulaAParsear, pattern);
            Assert.AreEqual(result, true);
        }
        [TestMethod]
        public void MultiplicacionSinSegundoOperandoIncorrecto()
        {
            string formulaAParsear = "a*";
            bool result = Regex.IsMatch(formulaAParsear, pattern);
            Assert.AreEqual(result, false);
        }
    }
}
