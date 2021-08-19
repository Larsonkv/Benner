using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;

namespace Projeto
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
           // Database.SetInitializer(new MigrateDatabaseToLatestVersion<Models.Context, Migrations.Configuration>()); // Migration
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
