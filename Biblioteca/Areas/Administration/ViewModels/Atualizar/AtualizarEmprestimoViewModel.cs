using Biblioteca.Models;
using Biblioteca.ViewModels;

namespace Biblioteca.Areas.Administration.ViewModels.Atualizar
{
    public class AtualizarEmprestimoViewModel : EmprestimoViewModel
    {
        public Guid Id { get; set; }
        public IEnumerable<Copia> CopiasExistentes { get; set; }
        public IEnumerable<Emprestimo> Emprestimos { get; set; }

        public List<Guid> IdCopias { get; set; }
    }
}
