using Biblioteca.Models;

namespace Biblioteca.Repositories.Interfaces
{
    public interface ICopiaRepository
    {
        public Task<IEnumerable<Copia>> GetCopiasDisponiveis();
        public Task<Copia?> GetCopiaById(Guid Id);

        public Task<IEnumerable<Copia>> GetCopias();

        public Task<Copia> CriarCopia(Copia copia);

        public Task<Copia> DeletarCopia(Copia copia);

        public Task<Copia> AtualizarCopia(Copia copia);
    }
}
