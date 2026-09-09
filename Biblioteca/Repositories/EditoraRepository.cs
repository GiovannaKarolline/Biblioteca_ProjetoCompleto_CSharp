using Biblioteca.Context;
using Biblioteca.Models;
using Biblioteca.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Repositories
{
    public class EditoraRepository : IEditoraRepository
    {
        private readonly BibliotecaDbContext _context;

        public EditoraRepository(BibliotecaDbContext context) 
        { 
            _context = context;
        }

        public async Task<Editora> AtualizarEditora(Editora editora)
        {
            _context.Editoras.Update(editora);
            _context.SaveChanges();

            return await Task.FromResult(editora);
        }

        public async Task<Editora> CriarEditora(Editora editora)
        {
            await _context.Editoras.AddAsync(editora);
            _context.SaveChanges();

            return await Task.FromResult(editora);
        }

        public async Task<Editora> DeletarEditora(Editora editora)
        {
            await AtualizarEditora(editora);

            return await Task.FromResult(editora);
        }

        public async Task<Editora?> GetEditoraById(Guid id)
        {
            return await _context.Editoras.FirstOrDefaultAsync(editora => editora.Id == id);
        }

        public async Task<IEnumerable<Editora>> GetEditoras() {
            return await _context.Editoras.ToListAsync();
        }
    }
}
