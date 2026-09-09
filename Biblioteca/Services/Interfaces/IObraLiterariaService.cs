using Biblioteca.Models;
using Biblioteca.ViewModels;

namespace Biblioteca.Services.Interfaces
{
    public interface IObraLiterariaService
    {
        public Task<ObraLiteraria> CriarObraLiteraria(ObraLiterariaViewModel obra);

        public Task<ObraLiteraria> DeletarObraLiteraria(Guid id);

        public Task<ObraLiteraria> AtualizarObraLiteraria(Guid id, ObraLiterariaViewModel obra);

        public Task<IEnumerable<ObraLiteraria>> GetObras();

        public Task<ObraLiteraria?> GetObraLiterariaById(Guid id);

        public Task<ObraLiteraria?> GetObraLiterariaByISBN(string ISBN);

    }
}
