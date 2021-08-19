using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Projeto.Helper;
using Projeto.Models;

namespace Projeto.Controllers
{
    public class UsuarioController : Controller
    {
        private Context db = new Context();

        // GET: Usuario
        public ActionResult Index()
        {
            var usuarios = db.Usuarios.Include(u => u.Papeis);
            return View(usuarios.ToList());
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
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

                if(usuario.ImagemUsuario != null)
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

        // GET: Usuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.PapelId = new SelectList(ComboHelper.GetPapeis(), "PapelId", "Nome", usuario.PapelId);
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if(usuario.ImagemUsuario != null)
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
                        ModelState.AddModelError(string.Empty, "Não é possivel salvar este Usuário porque já existe outro Usuário com o mesmo Email!");
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

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
            try
            {
                db.SaveChanges();
                return Json("Foi");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null &&
                                        ex.InnerException.InnerException != null &&
                                        ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                {
                    ModelState.AddModelError(string.Empty, "Não é possivel remover este Usuário porque existem Pedidos relacionados a ele, remova os Pedidos e tente novamente!");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

                return Json(usuario);
            }
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
