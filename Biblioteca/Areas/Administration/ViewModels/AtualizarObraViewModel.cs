using Biblioteca.Models;
using Biblioteca.ViewModels;

namespace Biblioteca.Areas.Administration.ViewModels
{
    public class AtualizarObraViewModel : ObraLiterariaViewModel
    {
        public Guid Id { get; set; }
        public IEnumerable<ObraLiteraria> Obras { get; set; }
    }
}
