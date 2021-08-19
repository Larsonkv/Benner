using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Projeto.Models
{
    public class Usuario
    {
        [Key]
        [Display(Name = "Id")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "O campo E-mail não pode ser vazio!")]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(250, ErrorMessage = "O campo E-mail não pode ter mais de 250 caracteres")]
        [Index("Usuario_Email_Index", IsUnique = true)]
        public String Email { get; set; }

        [Required(ErrorMessage = "O campo Senha não pode ser vazio!")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [MaxLength(250, ErrorMessage = "O campo Senha não pode ter mais de 250 caracteres")]
        public String Senha { get; set; }

        [Required(ErrorMessage = "O campo Nome não pode ser vazio!")]
        [Display(Name = "Nome")]
        [MaxLength(50, ErrorMessage = "O campo Nome não pode ter mais de 50 caracteres")]
        public String Nome { get; set; }

        [Required(ErrorMessage = "O campo Sobrenome não pode ser vazio!")]
        [Display(Name = "Sobrenome")]
        [MaxLength(50, ErrorMessage = "O campo Sobrenome não pode ter mais de 50 caracteres")]
        public String Sobrenome { get; set; }

        [Required(ErrorMessage = "O campo Telefone não pode ser vazio!")]
        [MaxLength(50, ErrorMessage = "O campo Telefone não pode ter mais de 50 caracteres")]
        [Display(Name = "Telefone")]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo Endereço não pode ser vazio!")]
        [MaxLength(50, ErrorMessage = "O campo Endereço não pode ter mais de 50 caracteres")]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Display(Name = "Imagem")]
        [DataType(DataType.ImageUrl)]
        public string Imagem { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImagemUsuario { get; set; }

        [Required(ErrorMessage = "O campo Papel não pode ser vazio!")]
        [Display(Name = "Papel")]
        [Range(1, double.MaxValue, ErrorMessage = "Selecione um Papel!")]
        public int PapelId { get; set; }

        [Display(Name = "Papel")]
        public virtual Papel Papeis { get; set; }
    }
}