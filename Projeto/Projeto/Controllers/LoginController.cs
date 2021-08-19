using Projeto.Helper;
using Projeto.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CaelumEstoque.Controllers
{
    public class LoginController : Controller
    {
        Context db = new Context();


        // GET: Login
        [FilterLogoutHelper]
        public ActionResult Index()
        {
            return View();
        }

        [FilterLogoutHelper]
        public ActionResult Autentica(String login, String senha)
        {
            var usuario = db.Usuarios.FirstOrDefault(p => p.Email == login && p.Senha == senha);
            var papel = db.Papels.Find(usuario.PapelId);

            if (papel.Nome == "Admin")
            {
                Session["adminLogado"] = usuario;
                return RedirectToAction("Index", "Produto");
            }
            if (papel.Nome == "Cliente")
            {
                Session["clienteLogado"] = usuario;
                return RedirectToAction("Index", "Home");
            }
            else
            {

                return RedirectToAction("Index");
            }
        }

        public ActionResult Sair()
        {
            Session["adminLogado"] = null;
            Session["clienteLogado"] = null;

            return RedirectToAction("Index");
        }
    }
}