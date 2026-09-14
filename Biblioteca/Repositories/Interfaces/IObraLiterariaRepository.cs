using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Repositories.Interfaces
{
    public interface IObraLiterariaRepository
    {
        public Task<IEnumerable<ObraLiteraria>> GetObras();

        public Task<IEnumerable<ObraLiteraria>> GetObrasLiterariasByTitulo(string titulo);

        public Task<ObraLiteraria?> GetObraLiterariaById(Guid id);

        public Task<ObraLiteraria?> GetObraLiterariaByISBN(string ISBN);

        public Task<ObraLiteraria> CriarObraLiteraria(ObraLiteraria obra);

        public Task<ObraLiteraria> DeletarObraLiteraria(ObraLiteraria obra);

        public Task<ObraLiteraria> AtualizarObraLiteraria(ObraLiteraria obra);
    }
}
