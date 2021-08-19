using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.Helper
{
    public class ComboHelper : IDisposable
    {
        private static Context db = new Context();

        public static List<Categoria> GetCategorias()
        {
            var categoria = db.Categorias.ToList();
            categoria.Add(new Categoria
            {
                CategoriaId = 0,
                Nome = "[ Selecione uma Categoria ]"
            });

            return categoria = categoria.OrderBy(o => o.Nome).ToList();

        }

        public static List<Papel> GetPapeis()
        {
            var papel = db.Papels.ToList();
            papel.Add(new Papel
            {
                PapelId = 0,
                Nome = "[ Selecione um Papel ]"
            });

            return papel = papel.OrderBy(o => o.Nome).ToList();

        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}