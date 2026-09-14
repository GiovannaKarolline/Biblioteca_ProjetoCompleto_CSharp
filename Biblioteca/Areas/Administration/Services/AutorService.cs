using Biblioteca.Models;
using Biblioteca.Repositories.Interfaces;
using Biblioteca.Areas.Administration.Services.Interfaces;
using Biblioteca.Areas.Administration.ViewModels;
using Microsoft.EntityFrameworkCore;
using Biblioteca.Areas.Administration.ViewModels.Atualizar;

namespace Biblioteca.Areas.Administration.Services
{
    public class AutorService : IAutorService
    {
        private readonly IAutorRepository _autorRepository;

        public AutorService(IAutorRepository autorRepository)
        {
            _autorRepository = autorRepository;
        }

        public async Task<Autor> AtualizarAutor(Guid id, AtualizarAutorViewModel autorViewModel)
        {
            var autor = await _autorRepository.GetAutorById(id);

            if (autor is not null)
            {
                autor.PrimeiroNome = autorViewModel.PrimeiroNome;
                autor.Sobrenome = autorViewModel.Sobrenome;

                await _autorRepository.AtualizarAutor(autor);
                return await Task.FromResult(autor);
            }
            throw new ArgumentNullException("O autor não pôde ser atualizado porque é nulo.");
        }

        public async Task<Autor> CriarAutor(AutorViewModel autorViewModel)
        {
            Autor novoAutor = new Autor()
            {
                PrimeiroNome = autorViewModel.PrimeiroNome,
                Sobrenome = autorViewModel.Sobrenome
            };

            if (novoAutor is not null)
            {
                await _autorRepository.CriarAutor(novoAutor);
                return await Task.FromResult(novoAutor);
            }
            throw new ArgumentNullException("O autor não pôde ser criado porque é nulo.");
        }

        public async Task<Autor> DeletarAutor(Guid id)
        {
            Autor? autor = await _autorRepository.GetAutorById(id);

            if (autor is not null)
            {
                autor.Deletado = true;
                await _autorRepository.DeletarAutor(autor);
            }
            else
            {
                throw new ArgumentNullException("O autor não pôde ser deletado porque ele já não existe no banco de dados.");
            }

            return await Task.FromResult(autor);
        }

        public async Task<Autor> GetAutorById(Guid id)
        {
            Autor? autor = (await _autorRepository.GetAutorById(id));

            if (autor is null || autor.Deletado == true)
            {
                throw new ArgumentException("Não existe um autor com este Id.");
            }

            return await Task.FromResult(autor);
        }

        public async Task<IEnumerable<Autor>> GetAutores()
        {
            var autores = await _autorRepository.GetAutores();

            autores = autores.Except(autores.Where(autor => autor.Deletado == true));

            return await Task.FromResult(autores);
        }
    }
}
