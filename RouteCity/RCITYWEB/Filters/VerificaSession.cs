using DBRouCity;
using RCITYWEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RCITYWEB.Models.Filters
{
    public class VerificaSession : ActionFilterAttribute
    {
        private Usuario oUsuario;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);
                //Aunque alli solo pongo session aqui tengo que agregar httpContext
                oUsuario = (Usuario)HttpContext.Current.Session["User"];
                if (oUsuario == null)
                {
                    if (filterContext.Controller is LoginController == null)
                    {
                        filterContext.HttpContext.Response.Redirect("/Login/Login");
                    }
                }
            }catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("~/Acceso/Login");            }
        }


    }
}