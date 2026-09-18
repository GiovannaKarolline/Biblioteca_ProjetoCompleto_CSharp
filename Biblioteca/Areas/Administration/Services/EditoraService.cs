using Biblioteca.Models;
using Biblioteca.Repositories.Interfaces;
using Biblioteca.Areas.Administration.Services.Interfaces;
using Biblioteca.Areas.Administration.ViewModels;
using Biblioteca.ViewModels;

namespace Biblioteca.Areas.Administration.Services
{
    public class EditoraService : IEditoraService
    {
        private readonly IEditoraRepository _editoraRepository;

        public EditoraService(IEditoraRepository editoraRepository)
        {
            _editoraRepository = editoraRepository;
        }

        public async Task<Editora> AtualizarEditora(Guid id, EditoraViewModel editora)
        {
            Editora? editoraRegistrada = await _editoraRepository.GetEditoraById(id);

            if (editoraRegistrada is not null)
            {
                editoraRegistrada.Nome = editora.Nome;

                await _editoraRepository.AtualizarEditora(editoraRegistrada);

                return await Task.FromResult(editoraRegistrada);
            }

            throw new ArgumentException("Não existe uma editora com este Id, então não foi possível aplicar a atualização.");
        }

        public async Task<Editora> CriarEditora(EditoraViewModel editora)
        {
            Editora? novaEditora = new Editora()
            {
                Nome = editora.Nome
            };

            if (novaEditora is null)
            {
                throw new ArgumentException("Não foi possível criar uma editora com o modelo fornecido.");
            }

            await _editoraRepository.CriarEditora(novaEditora);
            return await Task.FromResult(novaEditora);
        }

        public async Task<Editora> DeletarEditora(Guid id)
        {
            Editora? editora = await _editoraRepository.GetEditoraById(id);

            if (editora is null)
            {
                throw new ArgumentException("Não existe uma editora com este Id.");
            }

            editora.Deletado = true;
            await _editoraRepository.DeletarEditora(editora);

            return await Task.FromResult(editora);
        }

        public async Task<Editora?> GetEditoraById(Guid id)
        {
            Editora? editora = await _editoraRepository.GetEditoraById(id);

            if (editora is null || editora.Deletado == true)
            {
                throw new ArgumentException("Não existe uma editora com este Id no banco de dados.");
            }

            return editora;
        }

        public async Task<IEnumerable<Editora>> GetEditoras()
        {
            IEnumerable<Editora> editoras = await _editoraRepository.GetEditoras();

            editoras = editoras.Except(editoras.Where(editora => editora.Deletado == true));

            return await Task.FromResult(editoras);
        }
    }
}
