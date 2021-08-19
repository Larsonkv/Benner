using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Projeto.Models
{
    public class Context : DbContext
    {
        public Context() : base("DefaultConnection")
        {

        }

        // Desabilitar Cascatas
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public System.Data.Entity.DbSet<Projeto.Models.Categoria> Categorias { get; set; }

        public System.Data.Entity.DbSet<Projeto.Models.Produto> Produtos { get; set; }

        public System.Data.Entity.DbSet<Projeto.Models.Papel> Papels { get; set; }

        public System.Data.Entity.DbSet<Projeto.Models.Usuario> Usuarios { get; set; }
    }
}