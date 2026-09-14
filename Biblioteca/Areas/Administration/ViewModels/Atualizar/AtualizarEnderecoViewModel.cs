using Biblioteca.Models;
using Biblioteca.ViewModels;

namespace Biblioteca.Areas.Administration.ViewModels.Atualizar
{
    public class AtualizarEnderecoViewModel : EnderecoViewModel
    {
        public Guid Id { get; set; }

        public IEnumerable<Endereco>? Enderecos { get; set; }
    }
}
