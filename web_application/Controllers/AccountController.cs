using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Entities;
using BLL;

namespace web_application.Controllers
{
    public class AccountController : Controller
    {
        // GET: Acount
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Entities.Login data, string returnUrl)
        {
            //IngenetEntities db = new IngenetEntities();
            ActionResult Result;
            UsuariosBLL oBLL = new UsuariosBLL();
            List<Entities.Usuario> usuarios = oBLL.RetrieveAll();
            Entities.Usuario user = usuarios.FirstOrDefault(p => p.Correo == data.Correo && p.Contraseña == data.Contraseña && p.Estatus == 1);
            if (user != null)
            {
                if (user.Estatus == 1)
                {
                    Result = SignInUser(user, data.Rememberme, returnUrl);
                }
                else
                {
                    Result = View(data);
                }
            }
            else
            {
                Result = View(data);
            }
            return Result;
        }
        [AllowAnonymous]
        private ActionResult SignInUser(Entities.Usuario user, bool rememberMe, string returnUrl)
        {
            ActionResult Result;
            var Claims = new List<Claim>();
            Claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UsuarioID.ToString()));
            Claims.Add(new Claim(ClaimTypes.Email, user.Correo));
            Claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            Claims.Add(new Claim("fullname", user.Nombres));
            Claims.Add(new Claim(ClaimTypes.Role, user.Rol));
            /*
            if(user.Roles != & user.Roles.Any())
            {
                Claims.AddRange(user.Roles.Select(r => new Claim(ClaimTypes.Role, r.Name)));
            }
            */
            var Identity = new ClaimsIdentity(Claims, DefaultAuthenticationTypes.ApplicationCookie);

            IAuthenticationManager AuthenticationManager = HttpContext.GetOwinContext().Authentication;
            AuthenticationManager.SignIn(new AuthenticationProperties()
            {
                IsPersistent = rememberMe
            }, Identity);

            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = Url.Action("Index", "Home");
            }
            Result = Redirect(returnUrl);
            return Result;
        }
        [Authorize]
        public ActionResult LogOff()
        {
            IAuthenticationManager AuthenticationManager = HttpContext.GetOwinContext().Authentication;
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }
    }
}