using Biblioteca.Models;
using Biblioteca.ViewModels;

namespace Biblioteca.Areas.Administration.ViewModels
{
    public class DeletarEnderecoViewModel : EnderecoViewModel
    {
        public Guid Id { get; set; }
        public IEnumerable<Endereco>? Enderecos { get; set; }
    }
}
