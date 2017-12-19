using BLL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace web_application.Controllers
{
    public class PreguntaTemasController : Controller
    {
        // GET: PreguntaTemas
        public ActionResult Index()
        {
            PreguntaTemasBLL preguntaTemasBLL = new PreguntaTemasBLL();
            List<PreguntaTema> preguntaTemas = preguntaTemasBLL.RetrieveAll();
            return View(preguntaTemas);
        }

        public ActionResult Create()
        {
            TemasBLL temasBLL = new TemasBLL(); 
            List<Tema> temas= temasBLL.RetrieveAll();
            ViewBag.TemaID = new SelectList(temas, "TemaID", "NombreTema");
            PreguntasBLL preguntaBLL = new PreguntasBLL();
            List<Pregunta> preguntas = preguntaBLL.RetrieveAll();
            ViewBag.PreguntaID = new SelectList(preguntas, "PreguntaID", "TituloPregunta");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PreguntaTema  preguntaTema)
        {
            ActionResult Result;
            try
            {
                if (ModelState.IsValid)
                {
                    PreguntaTemasBLL oBLL = new PreguntaTemasBLL();
                    oBLL.Create(preguntaTema);
                    Result = RedirectToAction("Index");
                }
                else
                {
                    Result = View(preguntaTema);
                }
                return Result;
            }
            catch (Exception e)
            {
                return View(preguntaTema);
            }
        }
    }
}