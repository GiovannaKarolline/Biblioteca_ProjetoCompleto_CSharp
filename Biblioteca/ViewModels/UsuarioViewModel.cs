using Biblioteca.Enums;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.ViewModels
{
    public class UsuarioViewModel
    {
        [Required]
        [Display(Name = "Nome de usuário")]
        public string NomeUsuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required]
        public Cargo Cargo { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateOnly DataNascimento { get; set; }

        [Display(Name = "Número de Telefone")]
        [DataType(DataType.PhoneNumber)]
        public string NumeroTelefone { get; set; }
    }
}
