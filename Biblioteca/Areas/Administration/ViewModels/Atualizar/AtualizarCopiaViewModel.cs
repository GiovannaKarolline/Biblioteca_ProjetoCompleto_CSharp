using Biblioteca.Models;
using Biblioteca.ViewModels;

namespace Biblioteca.Areas.Administration.ViewModels.Atualizar
{
    public class AtualizarCopiaViewModel : CopiaViewModel
    {
        public Guid Id { get; set; }

        public IEnumerable<Copia>? Copias { get; set; }

        public IEnumerable<ObraLiteraria> Obras { get; set; }
    }
}
