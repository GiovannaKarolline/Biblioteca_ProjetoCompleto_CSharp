using Biblioteca.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Areas.Administration.ViewModels.Deletar
{
    public class DeletarObraViewModel : ObraLiterariaViewModel
    {
        [Required(ErrorMessage = "A obra literária a ser deletada precisa ser definida.")]
        public Guid Id { get; set; }
    }
}
