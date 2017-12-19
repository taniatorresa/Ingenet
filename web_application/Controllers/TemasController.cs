using BLL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web_application.Controllers
{
    public class TemasController : Controller
    {
        // GET: Categorias
        public ActionResult Index()
        {
            TemasBLL oBLL = new TemasBLL();
            List<Tema> temas = oBLL.RetrieveAll();
            return View(temas);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tema tema)
        {
            ActionResult Result;
            try
            {
                if (ModelState.IsValid)
                {
                    TemasBLL oBLL = new TemasBLL();
                    oBLL.Create(tema);
                    Result = RedirectToAction("Index");
                }
                else
                {
                    Result = View(tema);
                }
                return Result;
            }
            catch (Exception e)
            {
                return View(tema);
            }
        }

        public ActionResult Edit(int id)
        {
            TemasBLL oBLL = new TemasBLL();
            Tema tema = oBLL.Retrieve(id);

            return View(tema);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tema tema)
        {
            ActionResult Result;
            try
            {
                if (ModelState.IsValid)
                {
                    TemasBLL oBLL = new TemasBLL();
                    oBLL.Update(tema);
                    Result = RedirectToAction("Index");
                }
                else
                {
                    Result = View(tema);
                }
                return Result;
            }
            catch (Exception e)
            {
                return View(tema);
            }
        }

        public ActionResult Details(int id)
        {
            TemasBLL oBLL = new TemasBLL();
            Tema tema = oBLL.Retrieve(id);

            return View(tema);
        }

        public ActionResult Delete(int id)
        {
            TemasBLL oBLL = new TemasBLL();
            oBLL.Delete(id);
            return RedirectToAction("Index"); //redirecionar al index cuando borres
        }
    }
}