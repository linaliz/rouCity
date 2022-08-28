using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBRouCity;
using RCITYWEB.Domain.Util;
using RCITYWEB.Domain;
using RCITYWEB.Models;
using RCITYWEB.Operations;

namespace RCITYWEB.Controllers
{
    public class LoginController : Controller
    {
        private readonly RouCityEntities db = new RouCityEntities();
        UsuarioOper userOper = new UsuarioOper();
        public ActionResult Login()
        {
            // Si la cookie existe entonces el usuario esta logeado y lo redirigimos a la pagina principal
            if (Request.Cookies["cookie-login"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            Usuario usuario = new Usuario();
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string pass)
        {
            if (!String.IsNullOrEmpty(pass)) pass = CryptoUtil.base64Encode(CryptoUtil.EncodePasswordMd5(pass));


            if (ModelState.IsValid)
            {
                // Encuentro al usuario en bd que contenga el email y contraseña que nos ha pasado la view
                var oUser = (from d in db.Usuario
                             where d.email == email.Trim() && d.pass == pass.Trim()
                             select d).FirstOrDefault();
                            
               // bool user = db.Usuario.Any(u => u.email.ToString() == email && u.pass.ToString() == pass);

                if (oUser == null)
                {
                    ViewBag.Error = ViewBag.mensaje = "El email o contraseña son incorrectos";
                    return View();
                }

                Session["user"] = oUser;
                  
                    //si se encuentra un resultado en la base de datos que coincida con usuario y pass
                     //(por lo que no es necesario validar)
                        Response.Cookies["Sesion"]["email"] = email;
                        Response.Cookies["Sesion"]["pass"] = pass;
                        return RedirectToAction("Index", "Home");

                    
            }
            ViewBag.mensaje = "no entra";
            return View();
        }
      

    }

}

