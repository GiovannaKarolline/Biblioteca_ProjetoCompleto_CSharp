using Biblioteca.Models;

namespace Biblioteca.Areas.Administration.ViewModels
{
    public class AtualizarAutorViewModel
    {
        public string PrimeiroNome { get; set; }
        public string Sobrenome { get; set; }

        public Guid Id { get; set; }

        public IEnumerable<Autor>? Autores { get; set; }
    }
}
