using Biblioteca.Models;

namespace Biblioteca.Repositories.Interfaces
{
    public interface IEnderecoRepository
    {
        public Task<IEnumerable<Endereco>> GetEnderecos();

        public Task<Endereco?> GetEnderecoById(Guid id);

        public Task<Endereco> CriarEndereco(Endereco endereco);

        public Task<Endereco> DeletarEndereco(Endereco endereco);

        public Task<Endereco> AtualizarEndereco(Endereco endereco);
    }
}
