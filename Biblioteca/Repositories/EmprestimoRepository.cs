using Biblioteca.Context;
using Biblioteca.Models;
using Biblioteca.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace Biblioteca.Repositories
{
    public class EmprestimoRepository : IEmprestimoRepository
    {
        private readonly BibliotecaDbContext _context;

        public EmprestimoRepository(BibliotecaDbContext context)
        {
            _context = context;
        }

        public Task<Emprestimo> AtualizarEmprestimo(Emprestimo emprestimo)
        {
            _context.Emprestimos.Update(emprestimo);
            _context.SaveChanges();

            return Task.FromResult(emprestimo);
        }

        public Task<Emprestimo> CriarEmprestimo(Emprestimo emprestimo)
        {
            _context.Emprestimos.Add(emprestimo);
            _context.SaveChanges();

            return Task.FromResult(emprestimo);
        }

        public async Task<Emprestimo> DeletarEmprestimo(Emprestimo emprestimo)
        {
            await AtualizarEmprestimo(emprestimo);

            return await Task.FromResult(emprestimo);
        }

        public Task<Emprestimo?> GetEmprestimoById(Guid id)
        {
            return Task.FromResult(_context.Emprestimos
                .Include(emprestimo => emprestimo.Usuario)
                .Include(emprestimo => emprestimo.Copias)
                .ThenInclude(copia => copia.ObraLiteraria)
                .FirstOrDefault(emprestimo => emprestimo.Id == id));
        }

        public async Task<IEnumerable<Emprestimo>> GetEmprestimos()
        {
            return await _context.Emprestimos
                .Include(emprestimo => emprestimo.Usuario)
                .Include(emprestimo => emprestimo.Copias)
                .ThenInclude(copia => copia.ObraLiteraria)
                .ToListAsync();
        }

        public async Task<IEnumerable<Emprestimo>> GetEmprestimosByUsuarioId(Guid id)
        {
            return await _context.Emprestimos
                .Where(emprestimo => emprestimo.UsuarioId == id)
                .Include(emprestimo => emprestimo.Usuario)
                .Include(emprestimo => emprestimo.Copias)
                .ThenInclude(copia => copia.ObraLiteraria)
                .ToListAsync();
        }
    }
}
