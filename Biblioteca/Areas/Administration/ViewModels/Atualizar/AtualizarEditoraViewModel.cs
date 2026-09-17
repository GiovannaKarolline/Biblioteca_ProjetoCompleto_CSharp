using Biblioteca.Models;
using Biblioteca.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Areas.Administration.ViewModels.Atualizar
{
    public class AtualizarEditoraViewModel : EditoraViewModel
    {
        [Required(ErrorMessage = "A editora a ser atualizada precisa ser definida.")]
        public Guid Id { get; set; }
        public IEnumerable<Editora>? Editoras { get; set; }
    }
}
