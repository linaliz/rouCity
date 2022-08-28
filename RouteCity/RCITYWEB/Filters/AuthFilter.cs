using RCITYWEB.Domain;
using DBRouCity;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Agrodomo.Filters
{
    public class AuthFilter : AuthorizeAttribute
    {

        public AuthFilter()
        {

        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            var usuario = (Usuario)httpContext.Session["Usuario"];
            if (usuario != null)
            {
                String hash = new UsuarioDomain().ComprobarHash(usuario);
                if (hash != "")
                {
                    httpContext.Session["Usuario"] = usuario;
                    authorize = true;
                }

            }

            return authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    { "controller", "Login" },
                    { "action", "Login" }
               });
        }
    }
}