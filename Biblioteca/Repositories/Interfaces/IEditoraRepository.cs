using Biblioteca.Models;

namespace Biblioteca.Repositories.Interfaces
{
    public interface IEditoraRepository
    {
        public Task<IEnumerable<Editora>> GetEditoras();

        public Task<Editora?> GetEditoraById(Guid id);

        public Task<Editora> CriarEditora(Editora editora);

        public Task<Editora> DeletarEditora(Editora editora);

        public Task<Editora> AtualizarEditora(Editora editora);
    }
}
