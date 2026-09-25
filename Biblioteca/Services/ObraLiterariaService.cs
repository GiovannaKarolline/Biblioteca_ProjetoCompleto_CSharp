using Biblioteca.Areas.Administration.Services.Interfaces;
using Biblioteca.Models;
using Biblioteca.Repositories;
using Biblioteca.Repositories.Interfaces;
using Biblioteca.Services.Interfaces;
using Biblioteca.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Services
{
    public class ObraLiterariaService : IObraLiterariaService
    {
        private readonly IObraLiterariaRepository _obraRepository;
        private readonly IAutorService _autorService;

        public ObraLiterariaService(IObraLiterariaRepository obraRepository, IAutorService autorService)
        {
            _obraRepository = obraRepository;
            _autorService = autorService;
        }

        public async Task<ObraLiteraria> AtualizarObraLiteraria(Guid id, ObraLiterariaViewModel obraViewModel)
        {
            ObraLiteraria? obra = await _obraRepository.GetObraLiterariaById(id);

            if(obra is not null)
            {
                obra.Titulo = obraViewModel.Titulo;
                obra.ISBN = obraViewModel.ISBN;
                obra.AnoPublicacao = obraViewModel.AnoPublicacao;
                obra.CategoriaId = obraViewModel.CategoriaId;
                obra.EditoraId = obraViewModel.EditoraId;
                obra.FotoCapa = obraViewModel.FotoCapa;

                if(obra.Autores is null)
                {
                    obra.Autores = new List<Autor>();
                }
                
                foreach (Guid idAutor in obraViewModel.AutoresSelecionados)
                {
                    Autor autor = await _autorService.GetAutorById(idAutor);

                    if (!obra.Autores.Contains(autor))
                    {
                        obra.Autores.Add(autor);

                    }
                }

                List<Autor> autoresARemover = obra.Autores.Where(autor => !obraViewModel.AutoresSelecionados.Contains(autor.Id)).ToList();

                foreach (Autor autor in autoresARemover)
                {
                    obra.Autores.Remove(autor);
                }

                await _obraRepository.AtualizarObraLiteraria(obra);
            }
            else
            {
                throw new ArgumentException("Não é possível atualizar esta obra literária porque ela não existe no banco de dados.");
            }
            return await Task.FromResult(obra);
        }

        public async Task<ObraLiteraria> CriarObraLiteraria(ObraLiterariaViewModel obra)
        {
            ObraLiteraria novaObra = new ObraLiteraria()
            {
                Titulo = obra.Titulo,
                Autores = obra.Autores,
                AnoPublicacao = obra.AnoPublicacao,
                CategoriaId = obra.CategoriaId,
                EditoraId = obra.EditoraId,
                ISBN = obra.ISBN,
                FotoCapa = obra.FotoCapa
            };

            if(novaObra is not null)
            {
                await _obraRepository.CriarObraLiteraria(novaObra);
            }
            else
            {
                throw new ArgumentException("Não é possível criar essa obra literária porque ela é nula/inválida.");
            }

                return await Task.FromResult(novaObra);
        }

        public async Task<ObraLiteraria> DeletarObraLiteraria(Guid id)
        {
            var obra = await _obraRepository.GetObraLiterariaById(id);

            if (obra is not null)
            {
                obra.Deletado = true;

                await _obraRepository.DeletarObraLiteraria(obra);

                return await Task.FromResult(obra);
            }
            
            throw new ArgumentException("Esta obra não pôde ser apagada porque já não existe no banco de dados.");
        }

        public async Task<IEnumerable<ObraLiteraria>> GetObras()
        {
            IEnumerable<ObraLiteraria> obras = await _obraRepository.GetObras();

            obras = obras.Except(obras.Where(obra => obra.Deletado == true));

            return obras;
        }

        public async Task<ObraLiteraria?> GetObraLiterariaById(Guid id)
        {
            ObraLiteraria? obra = await _obraRepository.GetObraLiterariaById(id);

            if(obra is null || obra.Deletado == true)
            {
                throw new ArgumentException("Não existe uma obra literária com este Id no banco de dados.");
            }

            return obra;
        }

        public async Task<ObraLiteraria?> GetObraLiterariaByISBN(string ISBN)
        {
            ObraLiteraria? obra = await _obraRepository.GetObraLiterariaByISBN(ISBN);

            if (obra is null || obra.Deletado == true)
            {
                throw new ArgumentException("Não existe uma obra literária com este ISBN no banco de dados.");
            }

            return obra;
        }

        public async Task<IEnumerable<ObraLiteraria>> GetObrasLiterariasByTitulo(string titulo)
        {
            IEnumerable<ObraLiteraria> obras = await _obraRepository.GetObrasLiterariasByTitulo(titulo);

            obras = obras.Except(obras.Where(obra => obra.Deletado == true));

            return obras;
        }
    }
}
