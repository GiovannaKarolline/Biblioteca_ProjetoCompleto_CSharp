using Biblioteca.Enums;
using Biblioteca.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using X.PagedList;

namespace Biblioteca.ViewModels
{
    public class EnderecoViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O logradouro precisa ser preenchido.")]
        [StringLength(50)]
        public string Logradouro { get; set; }

        [StringLength(8, ErrorMessage = "O número pode ter de 1 a 8 caracteres")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "O CEP precisa ser preenchido.")]
        [StringLength(8, ErrorMessage = "O CEP possui 8 números. Insira um CEP válido.")]
        public string Cep { get; set; }

        [StringLength(50)]
        public string? Complemento { get; set; }

        [Required(ErrorMessage = "O tipo do logradouro precisa ser definido.")]
        public TipoLogradouro TipoLogradouro { get; set; }

        public IPagedList<Endereco>? Enderecos;

        public List<Usuario>? Usuarios;

        public Guid? UsuarioId { get; set; }
    }
}
