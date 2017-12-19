using BLL;
using Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace web_application.Controllers
{
    public class UsuarioController : Controller
    {
        class Global
        {
            public static int suces = 0;

        }

        // GET: Usuario
        [Authorize(Roles = "A")]
        public ActionResult Index()
        {
            UsuariosBLL oBLL = new UsuariosBLL();
            List<Usuario> usuarios = oBLL.RetrieveAll();
            return View(usuarios);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario, HttpPostedFileBase upFile)
        {
            ActionResult Result;
            try
            {
                if (ModelState.IsValid)
                {
                    if (upFile != null && upFile.ContentLength > 0)
                    {
                        int tamaño = upFile.ContentLength;
                        byte[] buffer = new byte[tamaño];
                        upFile.InputStream.Read(buffer, 0, tamaño);
                        usuario.Foto = buffer;
                    }
                    UsuariosBLL oBLL = new UsuariosBLL();
                    oBLL.Create(usuario);
                    Result = RedirectToAction("Index");
                }
                else
                {
                    Result = View(usuario);
                }
                return Result;
            }
            catch (Exception e)
            {
                return View(usuario);
            }
        }
        public ActionResult Edit(int id)
        {
            UsuariosBLL oBLL = new UsuariosBLL();
            Usuario usuario = oBLL.Retrieve(id);

            return View(usuario);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuario usuario, HttpPostedFileBase upFile)
        {
            ActionResult Result;
            try
            {

                Usuario tmpusuario = null;
                UsuariosBLL oBLL = new UsuariosBLL();

                if (ModelState.IsValid)
                {

                    tmpusuario = oBLL.Retrieve(usuario.UsuarioID);
                    tmpusuario.UserName = usuario.UserName;
                    tmpusuario.Rol = usuario.Rol;
                    tmpusuario.Nombres = usuario.Nombres;
                    tmpusuario.ApellidoMaterno = usuario.ApellidoMaterno;
                    tmpusuario.ApellidoPaterno = usuario.ApellidoPaterno;
                    tmpusuario.Contraseña = usuario.Contraseña;
                    tmpusuario.Sexo = usuario.Sexo;
                    tmpusuario.Ocupacion = usuario.Ocupacion;
                    tmpusuario.Carrera = usuario.Carrera;
                    tmpusuario.Descripción = usuario.Descripción;
                    tmpusuario.Correo = usuario.Correo;
                    tmpusuario.Estatus = usuario.Estatus;
                    Global.suces = 1;

                    if (upFile != null && upFile.ContentLength > 0)
                    {
                        int tamaño = upFile.ContentLength;
                        byte[] buffer = new byte[tamaño];
                        upFile.InputStream.Read(buffer, 0, tamaño);
                        tmpusuario.Foto = buffer;
                    }
                    else
                    {

                        tmpusuario.Foto = null;
                    }
                    oBLL.Update(tmpusuario);
                    Result = RedirectToAction("Details", new { id = usuario.UsuarioID });
                }
                else
                {
                    Result = RedirectToAction("Details", new { id = usuario.UsuarioID });
                }
                return Result;
            }
            catch (Exception e)
            {
                return View(usuario);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPerfilContraEstatus(Usuario usuario)
        {
            ActionResult Result;
            try
            {

                Usuario tmpusuario = null;
                UsuariosBLL oBLL = new UsuariosBLL();

                if (ModelState.IsValid)
                {

                    tmpusuario = oBLL.Retrieve(usuario.UsuarioID);
                    tmpusuario.UserName = usuario.UserName;
                    tmpusuario.Rol = usuario.Rol;
                    tmpusuario.Nombres = usuario.Nombres;
                    tmpusuario.ApellidoMaterno = usuario.ApellidoMaterno;
                    tmpusuario.ApellidoPaterno = usuario.ApellidoPaterno;
                    tmpusuario.Contraseña = usuario.Contraseña;
                    tmpusuario.Sexo = usuario.Sexo;
                    tmpusuario.Ocupacion = usuario.Ocupacion;
                    tmpusuario.Carrera = usuario.Carrera;
                    tmpusuario.Descripción = usuario.Descripción;
                    tmpusuario.Correo = usuario.Correo;
                    tmpusuario.Estatus = usuario.Estatus;
                    tmpusuario.Foto = usuario.Foto;

                    oBLL.Update(tmpusuario);
                    Global.suces = 1;
                    Result = RedirectToAction("Details", new { id = usuario.UsuarioID });
                }
                else
                {
                    Result = RedirectToAction("Details", new { id = usuario.UsuarioID });
                    Global.suces = 0;
                }
                return Result;
            }
            catch (Exception e)
            {
                return View(usuario);
            }
        }




        public ActionResult Details(int id)
        {
            UsuariosBLL oBLL = new UsuariosBLL();
            Usuario usuario = oBLL.Retrieve(id);
            if (Global.suces == 1)
            {
                ViewBag.success = 1;
            }
            else
            {
                ViewBag.success = 0;
            }
            Global.suces = 0;

            return View(usuario);
        }

        public ActionResult Delete(int id)
        {

            UsuariosBLL oBLL = new UsuariosBLL();
            oBLL.Delete(id);
            return Redirect(Url.Action("Login", "Account"));
        }

        public FileStreamResult GetImage(int id)
        {
            UsuariosBLL oBLL = new UsuariosBLL();
            Usuario usuario = oBLL.Retrieve(id);
            var bytes = usuario.Foto;
            return File(new MemoryStream(bytes, 0, bytes.Length), "image/jpeg");
        }

        public FileStreamResult Getknowimage(int id)
        {
            UsuariosBLL oBLL = new UsuariosBLL();
            Usuario usuario = oBLL.Retrieve(id);
            var bytes = usuario.Foto;
            var nuled = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
            if (bytes != null)
            {
                return File(new MemoryStream(bytes, 0, bytes.Length), "image/jpeg");
            }
            else
            {
                return File(new MemoryStream(nuled, 0, 0), "image/jpeg");
            }

        }

    }
}