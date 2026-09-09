using Biblioteca.Models;

namespace Biblioteca.Repositories.Interfaces
{
    public interface IEmprestimoRepository
    {
        public Task<IEnumerable<Emprestimo>> GetEmprestimos();

        public Task<Emprestimo?> GetEmprestimoById(Guid id);

        public Task<IEnumerable<Emprestimo>> GetEmprestimosByUsuarioId(Guid id);

        public Task<Emprestimo> CriarEmprestimo(Emprestimo emprestimo);

        public Task<Emprestimo> DeletarEmprestimo(Emprestimo emprestimo);

        public Task<Emprestimo> AtualizarEmprestimo(Emprestimo emprestimo);
    }
}
