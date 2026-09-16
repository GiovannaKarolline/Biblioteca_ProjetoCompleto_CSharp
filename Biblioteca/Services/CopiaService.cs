using Biblioteca.Models;
using Biblioteca.Repositories;
using Biblioteca.Repositories.Interfaces;
using Biblioteca.Services.Interfaces;
using Biblioteca.ViewModels;

namespace Biblioteca.Services
{
    public class CopiaService : ICopiaService
    {
        private readonly ICopiaRepository _copiaRepository;

        public CopiaService(ICopiaRepository copiaRepository)
        {
            _copiaRepository = copiaRepository;
        }

        public async Task<Copia> AtualizarCopia(Guid id, CopiaViewModel copia)
        {
            Copia? copiaRegistrada = await _copiaRepository.GetCopiaById(id);

            if(copiaRegistrada is not null)
            {
                copiaRegistrada.ObraLiterariaId = copia.ObraId;
                copiaRegistrada.StatusDisponibilidade = copia.StatusDisponibilidade;

                await _copiaRepository.AtualizarCopia(copiaRegistrada);

                return await Task.FromResult(copiaRegistrada);
            }

            throw new ArgumentException("Não existe uma cópia com este Id.");
        }

        public async Task<Copia> CriarCopia(CopiaViewModel copia)
        {
            Copia? novaCopia = new Copia()
            {
                ObraLiterariaId = copia.ObraId,
                StatusDisponibilidade = copia.StatusDisponibilidade
            };

            if(novaCopia is not null)
            {
                await _copiaRepository.CriarCopia(novaCopia);

                return await Task.FromResult(novaCopia);
            }

            throw new ArgumentException("Não foi possível criar uma cópia com os dados desse modelo.");
        }

        public async Task<Copia?> DeletarCopia(Guid id)
        {
            Copia? copia = await _copiaRepository.GetCopiaById(id);

            if (copia is not null)
            {
                copia.Deletado = true;
                await _copiaRepository.DeletarCopia(copia);

                return await Task.FromResult(copia);
            }

            throw new ArgumentException("A cópia não pôde ser deletada pois não existe no banco de dados ou não possui este Id.");
        }

        public async Task<Copia?> GetCopiaById(Guid id)
        {
            Copia? copia = await _copiaRepository.GetCopiaById(id);

            if(copia is not null && copia.Deletado == false)
            {
                return await Task.FromResult(copia);
            }

            return null;
        }

        public async Task<IEnumerable<Copia>> GetCopias()
        {
            IEnumerable<Copia> copias = await _copiaRepository.GetCopias();

            copias = copias.Where(copia => copia.Deletado == false);

            return copias;
        }

        public async Task<IEnumerable<Copia>> GetCopiasDisponiveis()
        {
            return await _copiaRepository.GetCopiasDisponiveis();
        }

        public async Task<IEnumerable<Copia>> EmprestarCopias(IEnumerable<Copia> copias)
        {

            if(copias is not null)
            {
                foreach(Copia copia in copias)
                {
                    if(copia.StatusDisponibilidade == true)
                    {
                        copia.StatusDisponibilidade = false;
                        await _copiaRepository.AtualizarCopia(copia);
                    }
                    else
                    {
                        throw new ArgumentException("Cópia indisponível: uma cópia já está emprestada");
                    }
                }

                return copias;
            }
            else
            {
                throw new ArgumentException("Lista de cópias nula.");
            }
        }
    }
}
