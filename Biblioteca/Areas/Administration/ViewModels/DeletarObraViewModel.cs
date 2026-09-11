using Biblioteca.Models;

namespace Biblioteca.Areas.Administration.ViewModels
{
    public class DeletarObraViewModel
    {
        public Guid Id { get; set; }
        public IEnumerable<ObraLiteraria> Obras { get; set; }
    }
}
