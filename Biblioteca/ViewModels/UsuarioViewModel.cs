using Biblioteca.Enums;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.ViewModels
{
    public class UsuarioViewModel
    {
        public string NomeUsuario { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [DataType(DataType.Date)]
        public DateOnly DataNascimento { get; set; }
        public Cargo Cargo { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required]
        public string NumeroTelefone { get; set; }
    }
}
