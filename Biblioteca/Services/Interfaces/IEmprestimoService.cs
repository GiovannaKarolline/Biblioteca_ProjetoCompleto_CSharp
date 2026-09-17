using Biblioteca.Models;
using Biblioteca.ViewModels;

namespace Biblioteca.Services.Interfaces
{
    public interface IEmprestimoService
    {
        public Task<IEnumerable<Emprestimo>> GetEmprestimos();

        public Task<Emprestimo?> GetEmprestimoById(Guid id);

        public Task<IEnumerable<Emprestimo>> GetEmprestimosByUsuarioId(Guid id);

        public Task<Emprestimo> CriarEmprestimo(EmprestimoViewModel emprestimo);

        public Task<Emprestimo> DeletarEmprestimo(Guid id);

        public Task<Emprestimo> AtualizarEmprestimo(Guid id, EmprestimoViewModel emprestimo);

        public Task<Emprestimo> AdicionarCopia(Guid idCopia, Guid idEmprestimo);

        public Task<Emprestimo> RemoverCopia(Guid idCopia, Guid idUsuario);

        public Task<Emprestimo> RemoverCopiaEmprestimoFinalizado(Guid idCopia, Guid idEmprestimo);

        public Task<Emprestimo?> FinalizarEmprestimo(Guid idEmprestimo);

        public Task<Emprestimo?> RealizarDevolucao(Guid idEmprestimo);
    }
}
