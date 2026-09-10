using Biblioteca.Models;
using Biblioteca.Areas.Administration.ViewModels;

namespace Biblioteca.Areas.Administration.Services.Interfaces
{
    public interface IAutorService
    {
        public Task<Autor> CriarAutor(AutorViewModel autorViewModel);

        public Task<Autor> DeletarAutor(Guid id);

        public Task<Autor> AtualizarAutor(Guid id, AtualizarAutorViewModel autorViewModel);

        public Task<IEnumerable<Autor>> GetAutores();
        public Task<Autor> GetAutorById(Guid id);
    }
}