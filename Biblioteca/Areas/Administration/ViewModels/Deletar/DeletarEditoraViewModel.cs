using Biblioteca.Models;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Areas.Administration.ViewModels.Deletar
{
    public class DeletarEditoraViewModel
    {
        [Required(ErrorMessage = "A editora a ser deletada precisa ser definida.")]
        public Guid Id { get; set; }

        public IEnumerable<Editora>? Editoras { get; set; }
    }
}
