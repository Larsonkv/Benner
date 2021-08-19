using System.Web.Mvc;
using Projeto.Helper;


namespace Projeto.Controllers
{
    
    public class HomeController : Controller
    {

        // GET: Home
        [FilterGenericHelper]
        public ActionResult Index()
        {
            return View();
        }
    }
}