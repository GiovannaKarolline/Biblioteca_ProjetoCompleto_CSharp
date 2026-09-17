using Biblioteca.Models;
using Biblioteca.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Areas.Administration.ViewModels.Deletar
{
    public class DeletarEmprestimoViewModel : EmprestimoViewModel
    {
        [Required(ErrorMessage = "O empréstimo a ser deletado precisa ser definido.")]
        public Guid Id { get; set; }

        public IEnumerable<Emprestimo> Emprestimos { get; set; }
    }
}
