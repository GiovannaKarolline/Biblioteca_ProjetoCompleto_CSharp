using Biblioteca.Models;
using Biblioteca.ViewModels;

namespace Biblioteca.Areas.Administration.ViewModels.Deletar
{
    public class DeletarEnderecoViewModel : EnderecoViewModel
    {
        public Guid Id { get; set; }
        public IEnumerable<Endereco>? Enderecos { get; set; }
    }
}
