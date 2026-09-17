using Biblioteca.Models;
using Biblioteca.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Areas.Administration.ViewModels.Deletar
{
    public class DeletarEnderecoViewModel : EnderecoViewModel
    {
        [Required(ErrorMessage = "O endereço a ser deletado precisa ser definido.")]
        public Guid Id { get; set; }
        public IEnumerable<Endereco>? Enderecos { get; set; }
    }
}
