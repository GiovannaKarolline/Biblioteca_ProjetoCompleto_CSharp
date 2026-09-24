using Biblioteca.Models;
using Biblioteca.ViewModels;

namespace Biblioteca.Services.Interfaces
{
    public interface IEnderecoService
    {
        public Task<IEnumerable<Endereco>> GetEnderecos();

        public Task<Endereco?> GetEnderecoById(Guid id);

        public Task<Endereco?> GetEnderecoByUsuarioId(Guid id);

        public Task<Endereco> CriarEndereco(EnderecoViewModel endereco);

        public Task<Endereco> DeletarEndereco(Guid id);

        public Task<Endereco> AtualizarEndereco(Guid id, EnderecoViewModel endereco);
    }
}
