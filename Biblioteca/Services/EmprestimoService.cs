using Biblioteca.Areas.Administration.ViewModels.Atualizar;
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
                emprestimoRegistrado.DataDevolucao = emprestimo.DataDevolucao;

                
                foreach(Copia copia in emprestimo.Copias.ToList())
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
                DataDevolucao = null,
                Finalizado = false
            };

            if(emprestimo.Copias is not null && emprestimo.Copias.Count() > 0)
            {
                foreach(Copia copia in emprestimo.Copias)
                {
                    novoEmprestimo.Copias.Add(copia);
                }
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
            Copia? copia = await _copiaService.GetCopiaById(idCopia);

            if(emprestimo is not null)
            {
                if(copia is not null)
                {
                    if(emprestimo.Copias is null)
                    {
                        emprestimo.Copias = new List<Copia>();

                        if(emprestimo.Copias.Select(copias => copias.Id == idCopia) is null)
                        {
                            emprestimo.Copias.Add(copia);
                        }
                    }
                    else
                    {
                        if(emprestimo.Copias.FirstOrDefault(copias => copias.Id == idCopia) is null)
                        {
                            emprestimo.Copias.Add(copia);
                        }
                    }
                }
                
                await _emprestimoRepository.AtualizarEmprestimo(emprestimo);

                return emprestimo;
            }

            throw new ArgumentException("Não existe um empréstimo com este Id no banco de dados.");
        }

        public async Task<Emprestimo> EditarCopiasEmprestimo(IEnumerable<Guid> idCopias, Guid idEmprestimo)
        {
            Emprestimo? emprestimo = await GetEmprestimoById(idEmprestimo);

            if (emprestimo is not null)
            {
                foreach (Guid idCopia in idCopias)
                {
                    if (!idCopia.Equals(Guid.Empty))
                    {
                        Copia? copia = await _copiaService.GetCopiaById(idCopia);

                        if(copia is not null)
                        {
                            if (!emprestimo.Copias.Any(copia => copia.Id == idCopia)) //se não tem nenhuma cópia no empréstimo com o Id da cópia selecionada
                            {
                                await AdicionarCopia(idCopia, emprestimo.Id);
                            }

                            if (emprestimo.Finalizado == true && emprestimo.DataDevolucao == null)
                            {
                                if (emprestimo.DataDevolucao is null)
                                {
                                    copia.StatusDisponibilidade = false;
                                }
                            }

                            if (emprestimo.Copias is null) //preencher o modelo
                            {
                                emprestimo.Copias = new List<Copia>();

                                if (copia is not null)
                                {
                                    emprestimo.Copias.Add(copia);
                                }
                            }
                            else
                            {
                                if (emprestimo.Copias.FirstOrDefault(copias => copias.Id == idCopia) is null && copia is not null)
                                {
                                    emprestimo.Copias.Add(copia);
                                }
                            }
                        }
                    }

                    List<Copia> copiasAtualizadas = new List<Copia>();

                    copiasAtualizadas.AddRange(emprestimo.Copias);

                    foreach (Copia copia in emprestimo.Copias) //copias que já estavam no empréstimo e as adicionadas anteriormente
                    {
                        if (!idCopias.Any(id => id == copia.Id)) //se houver alguma cópia nessa lista que não foi selecionada
                        {
                            copiasAtualizadas.Remove(copia);

                            if (emprestimo.Finalizado == true && emprestimo.DataDevolucao == null)
                            {
                                if (emprestimo.DataDevolucao is null)
                                {
                                    copia.StatusDisponibilidade = true;
                                }
                            }
                        }
                    }

                    emprestimo.Copias = copiasAtualizadas;
                    
                    await _emprestimoRepository.AtualizarEmprestimo(emprestimo);

                    return emprestimo;
                }
            }

            throw new ArgumentException("Não existe um empréstimo com este Id no banco de dados.");
        }

        public async Task<Emprestimo> RemoverCopia(Guid idCopia, Guid idUsuario)
        {
            Emprestimo? emprestimo = (await GetEmprestimosByUsuarioId(idUsuario)).FirstOrDefault(emprestimo => emprestimo.Finalizado == false);

            Copia? copia = await _copiaService.GetCopiaById(idCopia);

            if(copia is null)
            {
                throw new ArgumentException("Não existe registro de uma cópia com este Id.");
            }

            if(emprestimo is null)
            {
                throw new ArgumentException("Não existe registro de um empréstimo com este Id que não esteja finalizado.");
            }

            if (emprestimo.Copias.Contains(copia))
            {
                emprestimo.Copias.Remove(copia);
            }

            await _emprestimoRepository.AtualizarEmprestimo(emprestimo);

            return emprestimo;
        }

        public async Task<Emprestimo> RemoverCopiaEmprestimoFinalizado(Guid idCopia, Guid idEmprestimo)
        {
            Emprestimo? emprestimo = await GetEmprestimoById(idEmprestimo);

            Copia? copia = await _copiaService.GetCopiaById(idCopia);

            if (copia is null)
            {
                throw new ArgumentException("Não existe registro de uma cópia com este Id.");
            }

            if (emprestimo is null)
            {
                throw new ArgumentException("Não existe registro de um empréstimo com este Id.");
            }

            if (emprestimo.Copias.Contains(copia))
            {
                emprestimo.Copias.Remove(copia);
            }

            await _emprestimoRepository.AtualizarEmprestimo(emprestimo);

            return emprestimo;
        }

        public async Task<Emprestimo> FinalizarEmprestimo(Guid idEmprestimo)
        {
            Emprestimo? emprestimo = await GetEmprestimoById(idEmprestimo);

            if(emprestimo is null)
            {
                throw new ArgumentException("Não existe um empréstimo com este Id.");
            }

            emprestimo.Finalizado = true;

            foreach(Copia copia in emprestimo.Copias)
            {
                if(copia.StatusDisponibilidade == false)
                {
                    emprestimo.Copias.Remove(copia);

                    throw new ArgumentException("Não foi possível realizar o empréstimo. O empréstimo possuia cópias já emprestadas (agora removidas).");
                }

                copia.StatusDisponibilidade = false;
            }

            await _emprestimoRepository.AtualizarEmprestimo(emprestimo);

            return emprestimo;
        }

        public async Task<Emprestimo?> RealizarDevolucao(Guid idEmprestimo)
        {
            Emprestimo? emprestimo = await GetEmprestimoById(idEmprestimo);

            if (emprestimo.Copias is not null)
            {
                foreach (Copia copia in emprestimo.Copias)
                {
                    copia.StatusDisponibilidade = true;
                }
            }

            emprestimo.DataDevolucao = DateOnly.Parse(DateTime.Now.ToShortDateString());

            await _emprestimoRepository.AtualizarEmprestimo(emprestimo);

            return emprestimo;
        }
    }
}
