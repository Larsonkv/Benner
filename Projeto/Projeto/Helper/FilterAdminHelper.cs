using System.Web.Mvc;
using System.Web.Routing;

namespace Projeto.Helper
{
    public class FilterAdminHelper : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            object usuario = filterContext.HttpContext.Session["adminLogado"];
            if (usuario == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new { controller = "Login", action = "Index" }
                    )
                );
            }
        }
    }
}