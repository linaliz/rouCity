using System.Web;
using System.Web.Mvc;

namespace RCITYWEB
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new Models.Filters.VerificaSession());
        }
    }
}
