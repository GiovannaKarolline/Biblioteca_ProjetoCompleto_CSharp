using Biblioteca.Models;
using Biblioteca.ViewModels;

namespace Biblioteca.Areas.Administration.ViewModels.Atualizar
{
    public class AtualizarObraViewModel : ObraLiterariaViewModel
    {
        public Guid Id { get; set; }

        public IEnumerable<Categoria>? CategoriasExistentes { get; set; }

        public IEnumerable<Editora>? EditorasExistentes { get; set; }

        public IEnumerable<Autor>? AutoresExistentes { get; set; }
    }
}
