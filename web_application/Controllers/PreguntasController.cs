using web_application.Models;
using BLL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web_application.Controllers
{
    public class PreguntasController : Controller
    {
        // GET: Preguntas
        // GET: Categorias
        public ActionResult Index()
        {
            PreguntasBLL oBLL = new PreguntasBLL();
            List<Pregunta> preguntas = oBLL.RetrieveAll();
            return View(preguntas);
        }

        public ActionResult CreateSelect()
        {
            TemasBLL mBLL = new TemasBLL();
            List<Tema> tema = mBLL.RetrieveAll();
            List<SelectListItem> listSelect = new List<SelectListItem>();
            foreach (var tem in tema)
            {
                SelectListItem selectListItem = new SelectListItem()
                {
                    Text = tem.NombreTema,
                    Value = tem.TemaID.ToString(),
                    Selected = tem.IsSelected

                };
                listSelect.Add(selectListItem);

            }

            VMPregunta vMPregunta = new VMPregunta();
            vMPregunta.Temas = listSelect;
            return PartialView("_Select", vMPregunta);
        }

        public ActionResult Create()
        {
            UsuariosBLL obBLL = new UsuariosBLL();
            List<Usuario> usuarios = obBLL.RetrieveAll();
            ViewBag.UsuarioID = new SelectList(usuarios, "UsuarioID", "UserName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pregunta pregunta, VMPregunta vMPregunta)
        {
            var selectedTemas = vMPregunta.SelectedTemas;

            ActionResult Result;
            try
            {

                if (selectedTemas != null)
                {

                    foreach (var temaid in selectedTemas)
                    {
                        PreguntaTemasBLL mBLL = new PreguntaTemasBLL();
                        PreguntaTema tema = new PreguntaTema();
                        tema.TemaID = int.Parse(temaid);
                        tema.PreguntaID = pregunta.PreguntaID;
                        pregunta.PreguntaTemas.Add(tema);
                    }

                }
                if (ModelState.IsValid)
                {
                    PreguntasBLL oBLL = new PreguntasBLL();
                    oBLL.Create(pregunta);
                    return RedirectToAction("Index");
                }
                else
                {
                    Result = View(pregunta);
                }
                return Result;
            }
            catch (Exception e)
            {
                return View(pregunta);
            }
        }

        public string GetTema(int id)
        {
            TemasBLL mBLL = new TemasBLL();
            Tema tema = mBLL.Retrieve(id);
            var name = tema.NombreTema;

            return name;
        }
        public string GetTemas(int id)
        {
            TemasBLL mBLL = new TemasBLL();
            Tema tema = mBLL.Retrieve(id);
            var name = tema.NombreTema;

            return name;
        }
        public ActionResult ShowTemas(int id)
        {
      

            PreguntaTemasBLL preguntasBLL = new PreguntaTemasBLL();
            List<PreguntaTema> pregBLL = preguntasBLL.FilterPreguntasTemaByID(id);
            return PartialView("_ShowTemas",pregBLL);

        }

        public ActionResult Edit(int id)
        {
            PreguntasBLL oBLL = new PreguntasBLL();
            Pregunta pregunta= oBLL.Retrieve(id);

            return View(pregunta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pregunta pregunta)
        {
            ActionResult Result;
            try
            {
                if (ModelState.IsValid)
                {
                    PreguntasBLL oBLL = new PreguntasBLL();
                    oBLL.Update(pregunta);
                    Result = RedirectToAction("Index");
                }
                else
                {
                    Result = View(pregunta);
                }
                return Result;
            }
            catch (Exception e)
            {
                return View(pregunta);
            }
        }

        public ActionResult Details(int id)
        {
            PreguntasBLL oBLL = new PreguntasBLL();
            Pregunta pregunta= oBLL.Retrieve(id);
            ViewBag.PreguntaID = id;

            return View(pregunta);
        }

        public ActionResult Delete(int id)
        {
            PreguntasBLL oBLL = new PreguntasBLL();
            oBLL.Delete(id);
            return RedirectToAction("Index"); //redirecionar al index cuando borres
        }
        

        //Guarda y muesta la respuesta recien actualizada 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRespuesta(Pregunta pregunta)
        {

            ViewBag.PreguntaID = pregunta.PreguntaID;
            return View();
        }
        public ActionResult ShowRespuestas(int id)
        {
            RespuestasBLL listBLL = new RespuestasBLL();
            List<Respuesta> respuestas = listBLL.FilterRespuestasByPreguntaID(id);
            return PartialView("_ShowRespuestas",respuestas);
        }
        public ActionResult ShowNewRespuesta(Pregunta pregunta,Respuesta respuesta)
        {
            ActionResult Result;
            try
            {
                if (ModelState.IsValid)
                {
                    RespuestasBLL oBLL = new RespuestasBLL();
                    oBLL.Create(respuesta);
                    RespuestasBLL listBLL = new RespuestasBLL();
                    List<Respuesta> respuestas = listBLL.FilterRespuestasByPreguntaID(pregunta.PreguntaID);        
                    Result = PartialView("_ShowNewRespuesta",respuestas );
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
        public ActionResult BuscarPreguntasPorTema(Tema tema)
        {
            PreguntaTemasBLL preguntasBLL = new PreguntaTemasBLL();
            List<PreguntaTema> pregBLL = preguntasBLL.FilterPreguntasporTema(tema.TemaID);
            List<int> preguntasid = new List<int>();
            foreach (var  preg in pregBLL)
            {
                preguntasid.Add(preg.PreguntaID);
            };
            PreguntasBLL oBLL = new PreguntasBLL();
            List<Pregunta> preguntas = new List<Pregunta>();
            foreach( var item in preguntasid)
            {
                PreguntasBLL dBLL = new PreguntasBLL();
                preguntas.Add(dBLL.Retrieve(item));
            };

            return PartialView("_BuscarPreguntasPorTema",preguntas);
        }
        public ActionResult BuscarPreguntas()
        {
            TemasBLL obBLL = new TemasBLL();
            List<Tema> temas = obBLL.RetrieveAll();
            ViewBag.TemaID = new SelectList(temas, "TemaID", "NombreTema");
            return View();

        }



    }
}