using Biblioteca.Models;
using Biblioteca.ViewModels;

namespace Biblioteca.Services.Interfaces
{
    public interface IAutorService
    {
        public Task<Autor> CriarAutor(AutorViewModel autorViewModel);

        public Task<Autor> DeletarAutor(Guid id);

        public Task<Autor> AtualizarAutor(Guid id, AutorViewModel autorViewModel);

        public Task<IEnumerable<Autor>> GetAutores();
        public Task<Autor> GetAutorById(Guid id);
    }
}