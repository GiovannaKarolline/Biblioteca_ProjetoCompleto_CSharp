using Biblioteca.Models;
using Biblioteca.ViewModels;

namespace Biblioteca.Areas.Administration.ViewModels.Deletar
{
    public class DeletarEmprestimoViewModel : EmprestimoViewModel
    {
        public Guid Id { get; set; }
        public IEnumerable<Emprestimo> Emprestimos { get; set; }
    }
}
