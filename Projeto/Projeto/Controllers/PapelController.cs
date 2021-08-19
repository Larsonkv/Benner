using System.Linq;
using System.Web.Mvc;
using Projeto.Helper;
using Projeto.Models;

namespace Projeto.Controllers
{
    public class PapelController : Controller
    {
        private Context db = new Context();

        // GET: Papel
        [FilterGenericHelper]
        public ActionResult Index()
        {
            return View(db.Papels.ToList());
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
