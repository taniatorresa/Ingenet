using BLL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web_application.Controllers
{
    public class RespuestasController : Controller
    {
        // GET: Preguntas
        // GET: Categorias
        public ActionResult Index()
        {
            RespuestasBLL oBLL = new RespuestasBLL();
            List<Respuesta> respuestas = oBLL.RetrieveAll();
            return View(respuestas);
        }

        public ActionResult Create()
        {
            UsuariosBLL obBLL = new UsuariosBLL();
            List<Usuario> usuarios = obBLL.RetrieveAll();
            ViewBag.UsuarioID = new SelectList(usuarios, "UsuarioID", "UserName");
            PreguntasBLL preguntaBLL = new PreguntasBLL();
            List<Pregunta> preguntas = preguntaBLL.RetrieveAll();
            ViewBag.PreguntaID = new SelectList(preguntas, "PreguntaID", "TituloPregunta");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Respuesta respuesta)
        {
            ActionResult Result;
            try
            {
                if (ModelState.IsValid)
                {
                    RespuestasBLL oBLL = new RespuestasBLL();
                    oBLL.Create(respuesta);
                    Result = RedirectToAction("Index");
                }
                else
                {
                    Result = View(respuesta);
                }
                return Result;
            }
            catch (Exception e)
            {
                return View(respuesta);
            }
        }

        public ActionResult Edit(int id)
        {
            RespuestasBLL oBLL = new RespuestasBLL();
            Respuesta respuesta = oBLL.Retrieve(id);

            return View(respuesta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Respuesta respuesta)
        {
            ActionResult Result;
            try
            {
                if (ModelState.IsValid)
                {
                    RespuestasBLL oBLL = new RespuestasBLL();
                    oBLL.Update(respuesta);
                    Result = RedirectToAction("Index");
                }
                else
                {
                    Result = View(respuesta);
                }
                return Result;
            }
            catch (Exception e)
            {
                return View(respuesta);
            }
        }

        public ActionResult Details(int id)
        {
            RespuestasBLL oBLL = new RespuestasBLL();
            Respuesta respuesta = oBLL.Retrieve(id);

            return View(respuesta);
        }

        public ActionResult Delete(int id)
        {
            RespuestasBLL oBLL = new RespuestasBLL();
            oBLL.Delete(id);
            return RedirectToAction("Index"); //redirecionar al index cuando borres
        }
    
}
}