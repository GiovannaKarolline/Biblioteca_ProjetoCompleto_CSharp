using Biblioteca.Models;
using Biblioteca.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Areas.Administration.ViewModels.Atualizar
{
    public class AtualizarEnderecoViewModel : EnderecoViewModel
    {
        [Required(ErrorMessage = "O endereço a ser atualizado precisa ser definido.")]
        public Guid Id { get; set; }

        public IEnumerable<Endereco>? Enderecos { get; set; }
    }
}
