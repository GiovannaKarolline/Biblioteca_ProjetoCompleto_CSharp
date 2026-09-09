using Biblioteca.Context;
using Biblioteca.Models;
using Biblioteca.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Repositories
{
    public class CopiaRepository : ICopiaRepository
    {
        private readonly BibliotecaDbContext _context;
        public CopiaRepository(BibliotecaDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Copia>> GetCopiasDisponiveis()
        {
            return await _context.Copias.Where(copia => copia.StatusDisponibilidade == true).ToListAsync();
        }

        public Task<Copia> AtualizarCopia(Copia copia)
        {
            _context.Copias.Update(copia);

            return Task.FromResult(copia);
        }

        public async Task<Copia> CriarCopia(Copia copia)
        {
            await _context.Copias.AddAsync(copia);

            return await Task.FromResult(copia);
        }

        public Task<Copia> DeletarCopia(Copia copia)
        {
            AtualizarCopia(copia);
            _context.SaveChanges();

            return Task.FromResult(copia);
        }

        public async Task<Copia?> GetCopiaById(Guid id)
        {
            return await _context.Copias.FirstOrDefaultAsync(copia => copia.Id == id);
        }

        public async Task<IEnumerable<Copia>> GetCopias()
        {
            return await _context.Copias.Include(copia => copia.ObraLiteraria).ThenInclude(obra => obra.Categoria).ToListAsync();
        }
    }
}
