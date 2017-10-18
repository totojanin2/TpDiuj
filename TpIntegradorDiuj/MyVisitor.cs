using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr4.Runtime;
using TpIntegradorDiuj.ANTLR;
using Antlr4.Runtime.Misc;

namespace TpIntegradorDiuj
{
    class MyVisitor : Combined1BaseVisitor<double>
    {
        public override double VisitParentesis([NotNull] Combined1Parser.ParentesisContext context)
        {
            return base.Visit(context.expr());
        }
        public override double VisitSuma([NotNull] Combined1Parser.SumaContext context)
        {
            double left = Visit(context.expr(0));
            double right = Visit(context.expr(1));
            return left + right;
        }
        public override double VisitResta([NotNull] Combined1Parser.RestaContext context)
        {
            double left = Visit(context.expr(0));
            double right = Visit(context.expr(1));
            return left - right;
        }
        public override double VisitProducto([NotNull] Combined1Parser.ProductoContext context)
        {
            double left = Visit(context.expr(0));
            double right = Visit(context.expr(1));
            return left * right;
        }
        public override double VisitDivision([NotNull] Combined1Parser.DivisionContext context)
        {
            double left = Visit(context.expr(0));
            double right = Visit(context.expr(1));
            return left / right;
        }
        public override double VisitNumero([NotNull] Combined1Parser.NumeroContext context)
        {
            return double.Parse(context.num().GetText());
        }
        public override double VisitIndicador([NotNull] Combined1Parser.IndicadorContext context)
        {
            string indicadorBuscado = context.INDICADOR().GetText();
            // buscar en la db, el nombre del indicadorBuscado
            // if existe
            // return indicadorEncontrado.obtenerValor()
            // else
            // error
            return 0.5;
        }
    }
}