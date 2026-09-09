using Biblioteca.Context;
using Biblioteca.Models;
using Biblioteca.Repositories.Interfaces;
using Biblioteca.Services.Interfaces;
using Biblioteca.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public Task<Categoria> AtualizarCategoria(Guid id, CategoriaViewModel categoria)
        {
            if(_categoriaRepository.GetCategoriaById(id) is not null)
            {
                Categoria categoriaAtualizada = new Categoria()
                {
                    Titulo = categoria.Titulo
                };

                _categoriaRepository.AtualizarCategoria(categoriaAtualizada);

                return Task.FromResult(categoriaAtualizada);
            }
            throw new ArgumentException("Não foi possível atualizar esta categoria porque não existe uma categoria com este Id.");
        }

        public Task<Categoria> CriarCategoria(CategoriaViewModel categoria)
        {
            Categoria novaCategoria = new Categoria()
            {
                Titulo = categoria.Titulo
            };

            if(novaCategoria is not null)
            {
                _categoriaRepository.CriarCategoria(novaCategoria);
            }
            else
            {
                throw new ArgumentException("Não foi possível criar a categoria porque o modelo era inválido/nulo.");
            }

            return Task.FromResult(novaCategoria);

        }

        public async Task<Categoria?> DeletarCategoria(Guid id)
        {
            Categoria? categoria = await _categoriaRepository.GetCategoriaById(id);

            if (categoria is not null)
            {
                categoria.Deletado = true;
                await _categoriaRepository.DeletarCategoria(categoria);
            }
            else
            {
                throw new ArgumentException("Não foi possível deletar esta categoria porque não existe uma categoria com este Id.");
            }

            return await Task.FromResult(categoria);
        }

        public async Task<Categoria> GetCategoriaById(Guid id)
        {
            Categoria? categoria = await _categoriaRepository.GetCategoriaById(id);

            if (categoria is null || categoria.Deletado == true)
            {
                throw new ArgumentException("Não existe uma categoria com este Id.");
            }

            return await Task.FromResult(categoria);
        }

        public async Task<IEnumerable<Categoria>> GetCategorias()
        {
            var categorias = await _categoriaRepository.GetCategorias();

            categorias = categorias.Except(categorias.Where(categoria => categoria.Deletado == true));

            return categorias;
        }
    }
}
