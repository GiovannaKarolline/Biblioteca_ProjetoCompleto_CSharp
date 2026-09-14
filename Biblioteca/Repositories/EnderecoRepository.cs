using Biblioteca.Context;
using Biblioteca.Models;
using Biblioteca.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly BibliotecaDbContext _context;
        public EnderecoRepository(BibliotecaDbContext context)
        {
            _context = context;
        }

        public Task<Endereco> AtualizarEndereco(Endereco endereco)
        {
            _context.Enderecos.Update(endereco);
            _context.SaveChanges();

            return Task.FromResult(endereco);
        }

        public async Task<Endereco> CriarEndereco(Endereco endereco)
        {
            await _context.Enderecos.AddAsync(endereco);
            _context.SaveChanges();

            return endereco;
        }

        public Task<Endereco> DeletarEndereco(Endereco endereco)
        {
            AtualizarEndereco(endereco);

            return Task.FromResult(endereco);
        }

        public async Task<Endereco?> GetEnderecoById(Guid id)
        {
            return await _context.Enderecos.FirstOrDefaultAsync(endereco => endereco.Id == id);
        }

        public async Task<IEnumerable<Endereco>> GetEnderecos() 
        { 
            return await _context.Enderecos.Include(endereco => endereco.Usuario).ToListAsync(); 
        }
    }
}
