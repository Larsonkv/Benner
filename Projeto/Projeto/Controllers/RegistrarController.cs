using Projeto.Helper;
using Projeto.Models;
using System;
using System.Data.Entity;
using System.Web.Mvc;

namespace Projeto.Controllers
{
    public class RegistrarController : Controller
    {
        private Context db = new Context();

        // GET: Registrar
        public ActionResult Index()
        {
            return View();
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            ViewBag.PapelId = new SelectList(ComboHelper.GetPapeis(), "PapelId", "Nome");
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                db.SaveChanges();

                if (usuario.ImagemUsuario != null)
                {
                    var pic = string.Empty;
                    var folder = "~/Content/ImagemUsuario";
                    var file = string.Format("{0}.jpg", usuario.UsuarioId);
                    var response = FilesHelper.UploadPhoto(usuario.ImagemUsuario, folder, file);

                    if (response)
                    {
                        pic = string.Format("{0}/{1}", folder, file);
                        usuario.Imagem = pic;
                    }
                }
                db.Entry(usuario).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null &&
                       ex.InnerException.InnerException != null &&
                       ex.InnerException.InnerException.Message.Contains("_Index"))
                    {
                        ModelState.AddModelError(string.Empty, "Não é possivel criar este Usuário porque já existe outro Usuário com o mesmo Email!");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                    return View(usuario);
                }
            }
            ViewBag.PapelId = new SelectList(ComboHelper.GetPapeis(), "PapelId", "Nome", usuario.PapelId);
            return View(usuario);
        }
    }
}