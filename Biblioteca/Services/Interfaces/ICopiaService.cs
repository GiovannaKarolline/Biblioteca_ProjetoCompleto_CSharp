using Biblioteca.Models;
using Biblioteca.ViewModels;

namespace Biblioteca.Services.Interfaces
{
    public interface ICopiaService
    {
        public Task<IEnumerable<Copia>> GetCopiasDisponiveis();

        public Task<IEnumerable<Copia>> GetCopias();

        public Task<Copia> GetCopiaById(Guid id);

        public Task<Copia> CriarCopia(CopiaViewModel copia);

        public Task<Copia> AtualizarCopia(Guid id, CopiaViewModel copia);

        public Task<Copia?> DeletarCopia(Guid id);

        public Task<Copia> EmprestarCopia(Guid id);
    }
}
