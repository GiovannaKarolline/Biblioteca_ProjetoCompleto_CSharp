using Biblioteca.Models;
using Biblioteca.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Areas.Administration.ViewModels.Atualizar
{
    public class AtualizarObraViewModel : ObraLiterariaViewModel
    {
        [Required(ErrorMessage = "A obra literária a ser atualizada precisa ser definida.")]
        public Guid Id { get; set; }

        public IEnumerable<Categoria>? CategoriasExistentes { get; set; }

        public IEnumerable<Editora>? EditorasExistentes { get; set; }

        public IEnumerable<Autor>? AutoresExistentes { get; set; }
    }
}
