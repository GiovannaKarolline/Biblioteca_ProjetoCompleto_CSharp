using Biblioteca.Context;
using Biblioteca.Models;
using Biblioteca.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Repositories
{
    public class ObraLiterariaRepository : IObraLiterariaRepository
    {
        private readonly BibliotecaDbContext _context;

        public ObraLiterariaRepository(BibliotecaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ObraLiteraria>> GetObras()
        {
            return await _context.ObrasLiterarias.Include(obra => obra.Categoria).Include(obra => obra.Editora).ToListAsync();
        }

        public async Task<IEnumerable<ObraLiteraria>> GetObrasLiterariasByNome(string nome)
        {
            return await _context.ObrasLiterarias.Where(obraLiteraria => obraLiteraria.Titulo.Contains(nome)).ToListAsync();
        }

        public async Task<ObraLiteraria> CriarObraLiteraria(ObraLiteraria obra)
        {
            _context.ObrasLiterarias.Add(obra);
            _context.SaveChanges();

            return await Task.FromResult(obra);
        }

        public async Task<ObraLiteraria> DeletarObraLiteraria(ObraLiteraria obra)
        {
            await AtualizarObraLiteraria(obra);

            return await Task.FromResult(obra);
        }

        public async Task<ObraLiteraria> AtualizarObraLiteraria(ObraLiteraria obra)
        {
            _context.ObrasLiterarias.Update(obra);
            _context.SaveChanges();

            return await Task.FromResult(obra);
        }

        public async Task<ObraLiteraria?> GetObraLiterariaById(Guid id)
        {
            return await _context.ObrasLiterarias.Where(obra => obra.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ObraLiteraria?> GetObraLiterariaByISBN(string ISBN)
        {
            return await _context.ObrasLiterarias.Where(obra => obra.ISBN == ISBN).FirstOrDefaultAsync();
        }
    }
}
