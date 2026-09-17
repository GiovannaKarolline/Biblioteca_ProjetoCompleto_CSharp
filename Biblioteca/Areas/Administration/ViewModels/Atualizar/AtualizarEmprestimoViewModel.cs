using Biblioteca.Models;
using Biblioteca.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Areas.Administration.ViewModels.Atualizar
{
    public class AtualizarEmprestimoViewModel : EmprestimoViewModel
    {
        [Required(ErrorMessage = "O empréstimo a ser atualizado precisa ser definido.")]
        public Guid Id { get; set; }

        public IEnumerable<Copia> CopiasExistentes { get; set; }

        public IEnumerable<Emprestimo> Emprestimos { get; set; }

        [Required(ErrorMessage = "O empréstimo precisa incluir cópias para ser atualizado.")]
        public List<Guid> IdCopias { get; set; }
    }
}
