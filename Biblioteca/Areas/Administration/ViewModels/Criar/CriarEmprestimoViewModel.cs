using Biblioteca.Models;
using Biblioteca.ViewModels;

namespace Biblioteca.Areas.Administration.ViewModels.Criar
{
    public class CriarEmprestimoViewModel : EmprestimoViewModel
    {
        public IEnumerable<Copia>? CopiasExistentes { get; set; }
    }
}
