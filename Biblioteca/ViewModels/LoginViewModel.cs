using System.ComponentModel.DataAnnotations;

namespace Biblioteca.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O nome de usuário precisa ser preenchido.")]
        [Display(Name = "Nome de usuário")]
        public string NomeUsuario { get; set; }

        [Required(ErrorMessage = "A senha precisa ser devidamente preenchida.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
