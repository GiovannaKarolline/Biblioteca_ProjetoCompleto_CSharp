using Biblioteca.Context;
using Biblioteca.Models;
using Biblioteca.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Biblioteca.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly BibliotecaDbContext _context;

        public CategoriaRepository(BibliotecaDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Categoria>> GetCategorias()
        {
            return await _context.Categorias.ToListAsync();
        }

        public async Task<Categoria?> GetCategoriaById(Guid id)
        {
            return await _context.Categorias.Where(categoria => categoria.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Categoria> CriarCategoria(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();

            return categoria;
        }

        public async Task<Categoria> DeletarCategoria(Categoria categoria)
        {
            await AtualizarCategoria(categoria); 

            return await Task.FromResult(categoria);
        }

        public Task<Categoria> AtualizarCategoria(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            _context.SaveChanges();

            return Task.FromResult(categoria);
        }
    }
}