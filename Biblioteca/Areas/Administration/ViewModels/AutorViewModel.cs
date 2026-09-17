using Biblioteca.Models;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Areas.Administration.ViewModels
{
    public class AutorViewModel
    {
        [Required(ErrorMessage = "O primeiro nome do autor precisa ser preenchido.")]
        public string PrimeiroNome { get; set; }

        [Required(ErrorMessage = "O sobrenome do autor precisa ser preenchido")]
        public string Sobrenome { get; set; }
    }
}
