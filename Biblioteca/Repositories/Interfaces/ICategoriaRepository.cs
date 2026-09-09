using Biblioteca.Models;

namespace Biblioteca.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        public Task<IEnumerable<Categoria>> GetCategorias();

        public Task<Categoria?> GetCategoriaById(Guid id);

        public Task<Categoria> CriarCategoria(Categoria categoria);

        public Task<Categoria> DeletarCategoria(Categoria categoria);

        public Task<Categoria> AtualizarCategoria(Categoria categoria);
    }
}
