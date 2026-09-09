using Biblioteca.Enums;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.ViewModels
{
    public class EnderecoViewModel
    {
        [Required]
        [StringLength(50)]
        public string Logradouro { get; set; }

        [StringLength(8)]
        public string Numero { get; set; }

        [Required]
        [StringLength(8)]
        public string Cep { get; set; }

        [StringLength(50)]
        public string? Complemento { get; set; }

        [Required]
        public TipoLogradouro TipoLogradouro { get; set; }

        [Required]
        public Guid UsuarioId { get; set; }
    }
}
