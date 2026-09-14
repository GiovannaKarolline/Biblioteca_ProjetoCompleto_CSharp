using Biblioteca.Models;
using Biblioteca.Areas.Administration.ViewModels;
using Biblioteca.Areas.Administration.ViewModels.Atualizar;

namespace Biblioteca.Areas.Administration.Services.Interfaces
{
    public interface ICategoriaService
    {
        public Task<IEnumerable<Categoria>> GetCategorias();

        public Task<Categoria> GetCategoriaById(Guid id);

        public Task<Categoria> CriarCategoria(CategoriaViewModel categoria);

        public Task<Categoria> AtualizarCategoria(Guid id, AtualizarCategoriaViewModel categoria);

        public Task<Categoria?> DeletarCategoria(Guid id);
    }
}