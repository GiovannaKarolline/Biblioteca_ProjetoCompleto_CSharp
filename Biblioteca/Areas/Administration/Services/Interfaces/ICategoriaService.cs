using Biblioteca.Models;
using Biblioteca.Areas.Administration.ViewModels;

namespace Biblioteca.Areas.Administration.Services.Interfaces
{
    public interface ICategoriaService
    {
        public Task<IEnumerable<Categoria>> GetCategorias();

        public Task<Categoria> GetCategoriaById(Guid id);

        public Task<Categoria> CriarCategoria(CategoriaViewModel categoria);

        public Task<Categoria> AtualizarCategoria(Guid id, CategoriaViewModel categoria);

        public Task<Categoria?> DeletarCategoria(Guid id);
    }
}