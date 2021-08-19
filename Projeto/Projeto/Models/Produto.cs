using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Projeto.Models
{
    public class Produto
    {
        [Key]
        [Display(Name = "Id")]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "O campo Nome não pode ser vazio!")]
        [Display(Name = "Nome")]
        [MaxLength(50, ErrorMessage = "O campo Nome não pode ter mais de 50 caracteres")]
        [Index("Produto_Nome_Index", IsUnique = true)]
        public String Nome { get; set; }

        [Required(ErrorMessage = "O campo Quantidade não pode ser vazio!")]
        [Display(Name = "Quantidade")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "O campo Categoria não pode ser vazio!")]
        [Display(Name = "Categoria")]
        [Range(1, double.MaxValue, ErrorMessage = "Selecione uma Categoria!")]
        public int CategoriaId { get; set; }

        [Display(Name = "Categoria")]
        public virtual Categoria Categoria { get; set; }

        [Display(Name = "Imagem")]
        [DataType(DataType.ImageUrl)]
        public string Imagem { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImagemProduto { get; set; }

        [Required(ErrorMessage = "O campo Custo não pode ser vazio!")]
        [Display(Name = "Custo R$")]
        public float Custo { get; set; }

        [Required(ErrorMessage = "O campo Preço não pode ser vazio!")]
        [Display(Name = "Preço R$")]
        public float Preco { get; set; }
    }
}