using Biblioteca.Context;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Models
{
    [Table("Autores")]
    public class Autor
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Primeiro nome")]
        public string PrimeiroNome { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Sobrenome { get; set; } = string.Empty;

        public bool Deletado { get; set; } = false;

        public List<ObraLiteraria> ObrasLiterarias = new List<ObraLiteraria>();

        public override string ToString()
        {
            return $"\nAutor" +
                $"\nId do autor: {Id.ToString()}" +
                $"\nNome do autor: {PrimeiroNome + " " + Sobrenome}\n";
        }
    }
}
