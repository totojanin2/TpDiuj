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
        public void SumaRecursivaIndicadores()
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
            empresa.Balances.Add(bal);

            List<ComponenteOperando> lista = new List<ComponenteOperando>();
            lista.Add(cuenta);
            lista.Add(indicadorTest);
            int periodo = 2017;
            double result = indicador2.ObtenerValor(empresa,periodo,lista);
            Assert.AreEqual(result, 61);
        }
        [TestMethod]
        public void IndicadorMasCuentaPorValor()
        {
            Indicador indicadorTest = new Indicador();
            indicadorTest.Formula = "cuenta * 10";
            indicadorTest.Nombre = "ind";
            Cuenta cuenta = new Cuenta();
            cuenta.Nombre = "cuenta";
            cuenta.Valor = 50;
            Indicador indicador2 = new Indicador();
            indicador2.Formula = "ind + 25";
            Empresa empresa = new Empresa();
            Balance bal = new Balance();
            bal.Empresa = empresa;
            bal.Periodo = 2017;
            bal.Cuentas.Add(cuenta);
            empresa.Balances.Add(bal);

            List<ComponenteOperando> lista = new List<ComponenteOperando>();
            lista.Add(cuenta);
            lista.Add(indicadorTest);
            int periodo = 2017;
            double result = indicador2.ObtenerValor(empresa, periodo, lista);
            Assert.AreEqual(result, 525);
        }
        [TestMethod]
        public void SumaSoloCuenta() {
            Indicador indicadorTest = new Indicador();
            indicadorTest.Formula = "18 + terd";
            indicadorTest.Nombre = "ind";
            Cuenta cuenta = new Cuenta();
            cuenta.Nombre = "terd";
            cuenta.Valor = 50;
          
            Empresa empresa = new Empresa();
            Balance bal = new Balance();
            bal.Empresa = empresa;
            bal.Periodo = 2017;
            bal.Cuentas.Add(cuenta);
            empresa.Balances.Add(bal);

            List<ComponenteOperando> lista = new List<ComponenteOperando>();
            lista.Add(cuenta);
            lista.Add(indicadorTest);
            int periodo = 2017;
            double result = indicadorTest.ObtenerValor(empresa, periodo, lista);
            Assert.AreEqual(result, 68);
        }
        [TestMethod]
        public void MultiplicacionCuentaValor()
        {
            Indicador indicadorTest = new Indicador();
            indicadorTest.Formula = "10 * terd";
            indicadorTest.Nombre = "ind";
            Cuenta cuenta = new Cuenta();
            cuenta.Nombre = "terd";
            cuenta.Valor = 50;

            Empresa empresa = new Empresa();
            Balance bal = new Balance();
            bal.Empresa = empresa;
            bal.Periodo = 2017;
            bal.Cuentas.Add(cuenta);
            empresa.Balances.Add(bal);

            List<ComponenteOperando> lista = new List<ComponenteOperando>();
            lista.Add(cuenta);
            lista.Add(indicadorTest);
            int periodo = 2017;
            double result = indicadorTest.ObtenerValor(empresa, periodo, lista);
            Assert.AreEqual(result, 500);
        }
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
