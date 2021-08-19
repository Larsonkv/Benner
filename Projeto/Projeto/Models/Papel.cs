using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto.Models
{
    public class Papel
    {
        [Key]
        [Display(Name = "Id")]
        public int PapelId { get; set; }

        [Required(ErrorMessage = "O campo Nome não pode ser vazio!")]
        [MaxLength(50, ErrorMessage = "O campo Nome não pode ter mais de 50 caracteres")]
        [Display(Name = "Nome")]
        [Index("Papel_Nome_Index", IsUnique = true)]
        public string Nome { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}