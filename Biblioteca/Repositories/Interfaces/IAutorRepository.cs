using Biblioteca.Models;

namespace Biblioteca.Repositories.Interfaces
{
    public interface IAutorRepository
    {
        public Task<IEnumerable<Autor>> GetAutores();

        public Task<Autor?> GetAutorById(Guid id);

        public Task<Autor> CriarAutor(Autor autor);

        public Task<Autor> DeletarAutor(Autor autor);

        public Task<Autor> AtualizarAutor(Autor autor);
    }
}
