using Biblioteca.Models;
using Biblioteca.Repositories.Interfaces;
using Biblioteca.Services.Interfaces;
using Biblioteca.ViewModels;

namespace Biblioteca.Services
{
    public class EmprestimoService : IEmprestimoService
    {
        private readonly IEmprestimoRepository _emprestimoRepository;
        private readonly ICopiaService _copiaService;
        public EmprestimoService(IEmprestimoRepository emprestimoRepository, ICopiaService copiaService)
        {
            _emprestimoRepository = emprestimoRepository;
            _copiaService = copiaService;
        }

        public async Task<Emprestimo> AtualizarEmprestimo(Guid id, EmprestimoViewModel emprestimo)
        {
            Emprestimo? emprestimoRegistrado = await _emprestimoRepository.GetEmprestimoById(id);

            if(emprestimoRegistrado is not null && emprestimoRegistrado.Deletado == false)
            {
                emprestimoRegistrado.UsuarioId = emprestimo.UsuarioId;
                emprestimoRegistrado.DataRetirada = emprestimo.DataRetirada;
                emprestimoRegistrado.DataPrevistaDevolucao = emprestimo.DataDevolucao;
                emprestimoRegistrado.DataDevolucao = emprestimo.DataDevolucao;
                
                foreach(Copia copia in emprestimo.Copias)
                {
                    emprestimoRegistrado.Copias.Add(await _copiaService.GetCopiaById(copia.Id));
                }

                await _emprestimoRepository.AtualizarEmprestimo(emprestimoRegistrado);

                return emprestimoRegistrado;
            }

            throw new ArgumentException("O empréstimo não pôde ser atualizado (modelo possivelmente contém conteúdo inválido).");
        }

        public async Task<Emprestimo> CriarEmprestimo(EmprestimoViewModel emprestimo)
        {
            Emprestimo? novoEmprestimo = new Emprestimo()
            {
                UsuarioId = emprestimo.UsuarioId,
                DataPrevistaDevolucao = DateOnly.Parse(DateTime.Now.ToShortDateString()).AddMonths(3),
                DataRetirada = DateOnly.Parse(DateTime.Now.ToShortDateString()),
                DataDevolucao = new DateOnly(),
                Finalizado = true
            };

            foreach(Copia copia in emprestimo.Copias)
            {
                novoEmprestimo.Copias.Add(copia);
            }

            if(novoEmprestimo is not null)
            {
                await _emprestimoRepository.CriarEmprestimo(novoEmprestimo);

                return novoEmprestimo;
            }

            throw new ArgumentException("O empréstimo não pôde ser criado (modelo possivelmente contém conteúdo inválido).");
        }

        public async Task<Emprestimo> DeletarEmprestimo(Guid id)
        {
            Emprestimo? emprestimo = await _emprestimoRepository.GetEmprestimoById(id);

            if(emprestimo == null || emprestimo.Deletado == true)
            {
                throw new ArgumentException("O emprestimo não pôde ser deletado porque não existe no banco de dados.");
            }

            emprestimo.Deletado = true;
            await _emprestimoRepository.DeletarEmprestimo(emprestimo);

            return emprestimo;
        }

        public async Task<Emprestimo?> GetEmprestimoById(Guid id)
        {
            Emprestimo? emprestimo = await _emprestimoRepository.GetEmprestimoById(id);

            if(emprestimo is null || emprestimo.Deletado == true)
            {
                throw new ArgumentException("Não existe um empréstimo com este Id no banco de dados.");
            }

            return emprestimo;
        }

        public async Task<IEnumerable<Emprestimo>> GetEmprestimos()
        {
            IEnumerable<Emprestimo> emprestimos = await _emprestimoRepository.GetEmprestimos();

            emprestimos = emprestimos.Except(emprestimos.Where(emprestimo => emprestimo.Deletado == true));

            return emprestimos;
        }

        public async Task<IEnumerable<Emprestimo>> GetEmprestimosByUsuarioId(Guid id)
        {
            IEnumerable<Emprestimo> emprestimos = await _emprestimoRepository.GetEmprestimosByUsuarioId(id);

            emprestimos = emprestimos.Except(emprestimos.Where(emprestimo => emprestimo.Deletado == true));

            return emprestimos;
        }

        public async Task<Emprestimo> AdicionarCopia(Guid idCopia, Guid idEmprestimo)
        {
            Emprestimo? emprestimo = await GetEmprestimoById(idEmprestimo);

            if(emprestimo is not null)
            {
                emprestimo.Copias.Add(await _copiaService.GetCopiaById(idCopia));

                await _emprestimoRepository.AtualizarEmprestimo(emprestimo);

                return emprestimo;
            }

            throw new ArgumentException("Não existe um empréstimo com este Id no banco de dados.");
        }

    }
}
