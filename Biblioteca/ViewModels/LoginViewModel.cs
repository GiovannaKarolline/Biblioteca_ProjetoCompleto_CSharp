using System.ComponentModel.DataAnnotations;

namespace Biblioteca.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Nome de usuário")]
        public string NomeUsuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
