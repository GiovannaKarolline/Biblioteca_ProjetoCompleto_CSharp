using Biblioteca.Models;
using Biblioteca.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Areas.Administration.ViewModels.Criar
{
    public class CriarEmprestimoViewModel : EmprestimoViewModel
    {
        public IEnumerable<Copia>? CopiasExistentes { get; set; }

        [Required(ErrorMessage = "O empréstimo precisa possuir cópias para ser criado.")]
        public IEnumerable<Guid> idCopias { get; set; }
    }
}
