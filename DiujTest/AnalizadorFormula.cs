using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace DiujTest
{
    [TestClass]
    public class AnalizadorFormula
    {
        const string pattern = @"((\b([A-z]*[0-9]*|[0-9]*[A-z]*)[a-z0-9]*\b)([+\-\*\/]\(?(\b([a-z]*[0-9]*|[0-9]*[a-z]*)[a-z0-9]*\b)\)?)+)";
        [TestMethod]
        public void SumaSimpleEsCorrecto()
        {
            string formulaAParsear = "a+b";
            bool result = Regex.IsMatch(formulaAParsear, pattern);
            Assert.AreEqual(result, true);
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
