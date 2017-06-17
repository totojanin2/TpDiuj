
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TpIntegradorDiuj.Models;

namespace TpIntegradorDiuj.Controllers
{
    public class IndicadoresController : Controller
    {
        // GET: Indicadores
        public ActionResult Index()
        {
            ViewBag.ListIndicadores = DeserializarArchivoIndicadores().Select(x => new SelectListItem {
                Text = x.Nombre,
                Value = x.Id.ToString()
            }).ToList();
            
            return View();
        }
        public List<Indicador> DeserializarArchivoIndicadores()
        {
            string buf = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/Archivos/") + "indicadores.json");
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Indicador> listIndicadores = serializer.Deserialize<List<Indicador>>(buf);
            return listIndicadores;

        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ObtenerFormulaDelIndicador(int idIndicador)
        {
            var indicador = DeserializarArchivoIndicadores().FirstOrDefault(x => x.Id == idIndicador);
            return Json(new { Formula = indicador.Formula });
        }
        [HttpPost]
        public ActionResult Create(Indicador model)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Indicador>  indicadores=DeserializarArchivoIndicadores();
            int maxId = indicadores.Select(x => x.Id).Max();
            model.Id = maxId+1;
            indicadores.Add(model);
            string jsonData = JsonConvert.SerializeObject(indicadores);
            System.IO.File.WriteAllText(Server.MapPath("~/App_Data/Archivos/") + "indicadores.json", jsonData);
            return RedirectToAction("Index");
        }
        public CalcularValorByFormula(int id)
        {

            var formula = DeserializarArchivoIndicadores().FirstOrDefault(x => x.Id == id).Formula;
            string input = formula;
            ICharStream stream = new StreamReader(file.InputStream);
            ITokenSource lexer = new FormulasLexer(stream);
            ITokenStream tokens = new CommonTokenStream(lexer);
            FormulasParser parser = new FormulasParser(tokens);
          //  parser.buildParseTrees = true;
            IParseTree tree = parser.StartRule();
        }
    }
}