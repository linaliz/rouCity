using DBRouCity;
using RCITYWEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RCITYWEB.Models.Filters
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =false)]
    public class AutorizacionUser : AuthorizeAttribute
    {
        private Usuario oUsuario;
        private RouCityEntities db = new RouCityEntities();
        private int idOperacion;

        public AutorizacionUser(int idOperacion = 0)
        {
            this.idOperacion = idOperacion;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            String nombreOperacion = "";
            String nombreModulo = "";

            try
            {
                oUsuario = (Usuario)HttpContext.Current.Session["user"];

        
            }catch (Exception ex)
            {

            }
        }

    }
    }
