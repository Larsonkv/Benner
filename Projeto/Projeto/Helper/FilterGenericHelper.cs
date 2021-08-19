using System.Web.Mvc;
using System.Web.Routing;

namespace Projeto.Helper
{
    public class FilterGenericHelper : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            object admin = filterContext.HttpContext.Session["adminLogado"];
            object cliente = filterContext.HttpContext.Session["clienteLogado"];
            if (admin == null && cliente == null)
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