using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Projeto.Helper;
using Projeto.Models;

namespace Projeto.Controllers
{
    public class ProdutoController : Controller
    {
        private Context db = new Context();

        // GET: Produto
        
        [FilterGenericHelper]
        public ActionResult Index()
        {

            var produtos = db.Produtos.Include(p => p.Categoria);
            return View(produtos.ToList());
        }

        // GET: Produto/Details/5
        [FilterAdminHelper]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtos.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // GET: Produto/Create
        [FilterAdminHelper]
        public ActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(ComboHelper.GetCategorias(), "CategoriaId", "Nome");
            return View();
        }

        // POST: Produto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [FilterAdminHelper]
        public ActionResult Create(Produto produto)
        {
            if (ModelState.IsValid)
            {
                db.Produtos.Add(produto);
                db.SaveChanges();

                if(produto.ImagemProduto != null)
                {
                    var pic = string.Empty;
                    var folder = "~/Content/ImagemProduto";
                    var file = string.Format("{0}.jpg", produto.ProdutoId);
                    var response = FilesHelper.UploadPhoto(produto.ImagemProduto, folder, file);

                    if (response)
                    {
                        pic = string.Format("{0}/{1}", folder, file);
                        produto.Imagem = pic;
                    }
                }
                db.Entry(produto).State = EntityState.Modified;
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
                        ModelState.AddModelError(string.Empty, "Não é possivel criar este Produto porque já existe outro Produto com o mesmo nome!");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                    return View(produto);
                }

            }

            ViewBag.CategoriaId = new SelectList(ComboHelper.GetCategorias(), "CategoriaId", "Nome", produto.CategoriaId);
            return View(produto);
        }

        // GET: Produto/Edit/5
        [FilterAdminHelper]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtos.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaId = new SelectList(ComboHelper.GetCategorias(), "CategoriaId", "Nome", produto.CategoriaId);
            return View(produto);
        }

        // POST: Produto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [FilterAdminHelper]
        public ActionResult Edit(Produto produto)
        {
            if (ModelState.IsValid)
            {
                if (produto.ImagemProduto != null)
                {
                    var pic = string.Empty;
                    var folder = "~/Content/ImagemProduto";
                    var file = string.Format("{0}.jpg", produto.ProdutoId);

                    var response = FilesHelper.UploadPhoto(produto.ImagemProduto, folder, file);
                    if (response)
                    {
                        pic = string.Format("{0}/{1}", folder, file);
                        produto.Imagem = pic;

                    }
                }
                db.Entry(produto).State = EntityState.Modified;
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
                        ModelState.AddModelError(string.Empty, "Não é possivel salvar este Produto porque já existe outro Produto com o mesmo nome!");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                    return View(produto);
                }
            }
            ViewBag.CategoriaId = new SelectList(ComboHelper.GetCategorias(), "CategoriaId", "Nome", produto.CategoriaId);
            return View(produto);
        }

        // POST: Produto/Delete/5
        [HttpPost, ActionName("Delete")]
        [FilterAdminHelper]
        public ActionResult DeleteConfirmed(int id)
        {
            Produto produto = db.Produtos.Find(id);
            db.Produtos.Remove(produto);
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
                    ModelState.AddModelError(string.Empty, "Não é possivel remover este Produto porque existem Pedidos relacionados a ele, remova os Pedidos e tente novamente!");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

                return Json(produto);
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
