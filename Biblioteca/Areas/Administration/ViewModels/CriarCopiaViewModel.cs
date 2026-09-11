using Biblioteca.Models;
using Biblioteca.ViewModels;

namespace Biblioteca.Areas.Administration.ViewModels
{
    public class CriarCopiaViewModel : CopiaViewModel
    {
        public IEnumerable<ObraLiteraria>? ObrasPossiveis { get; set; }
    }
}