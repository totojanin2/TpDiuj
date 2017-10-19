using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace TpIntegradorDiuj.Models
{
    public class Indicador : ComponenteOperando
    {
        const string pattern = @"((\b([A-z]*[0-9]*|[0-9]*[A-z]*)[a-z0-9]*\b)([+\-\*\/]\(?(\b([a-z]*[0-9]*|[0-9]*[a-z]*)[a-z0-9]*\b)\)?)+)";
        public string Formula { get; set; }
        public string UsuarioCreador_Id { get; set; }
        //public virtual ApplicationUser UsuarioCreador { get; set; }
        public List<ComponenteOperando> Operandos { get; set; }
        public override double ObtenerValor(Empresa empresa, int periodo)
        {
            double result = 0;
            //Parsear la formula
            //Aplicar la formula
            return result;
        }
        public void ValidarExpresionFormula(IEnumerable<Indicador> indicadores)
        {
            //GramaticaLexer lexer = new GramaticaLexer(new AntlrInputStream(this.Formula));
            //CommonTokenStream tokens = new CommonTokenStream(lexer);
            //GramaticaParser parser = new GramaticaParser(tokens);
            //parser.BuildParseTree = true;
            //GramaticaParser.IndicadorContext indicadorContext = parser.indicador();
            //ParseTreeWalker walker = new ParseTreeWalker();
            //Listener listener = new Listener(textBox2.Text);
            //walker.Walk(listener, indicadorContext);

            ////lo muestro en el tree view
            //treeView1.Nodes.Clear();
            //BinaryTreeNode<IContenidoNodo> nodo = listener.ArbolBinario.Root;
            //treeView1.Nodes.Add(agregarAlTreeView(nodo));

            ////guardar Listener.ArbolBinario
            //Indicadores.listaDeIndicadores.Add(listener.ArbolBinario);
        }
    }
}