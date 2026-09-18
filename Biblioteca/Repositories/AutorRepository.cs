using Biblioteca.Context;
using Biblioteca.Models;
using Biblioteca.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        private readonly BibliotecaDbContext _context;

        public AutorRepository(BibliotecaDbContext context)
        {
            _context = context;
        }

        public async Task<Autor?> GetAutorById(Guid id)
        {
            return await _context.Autores.FirstOrDefaultAsync(autor => autor.Id == id);
        }

        public async Task<IEnumerable<Autor>> GetAutores() 
        { 
            return await _context.Autores.ToListAsync(); 
        }

        public async Task<Autor> DeletarAutor(Autor autor)
        {
            AtualizarAutor(autor);

            return await Task.FromResult(autor);
        }

        public Task<Autor> AtualizarAutor(Autor autor)
        {
            _context.Autores.Update(autor);
            _context.SaveChanges();

            return Task.FromResult(autor);
        }

        public Task<Autor> CriarAutor(Autor autor)
        {
            _context.Autores.Add(autor);
            _context.SaveChanges();

            return Task.FromResult(autor);
        }

    }
}
