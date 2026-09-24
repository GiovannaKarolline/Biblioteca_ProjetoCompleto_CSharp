using Biblioteca.Enums;
using Biblioteca.Models;
using System.ComponentModel.DataAnnotations;
using X.PagedList;

namespace Biblioteca.ViewModels
{
    public class UsuarioViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome de usuário precisa ser fornecido.")]
        [Display(Name = "Nome de usuário")]
        public string NomeUsuario { get; set; }

        [Required(ErrorMessage = "A senha precisa ser fornecida.")]
        [DataType(DataType.Password, ErrorMessage = "A senha precisa ser válida (pelo menos 6 caracteres, caractere especial e números)")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O cargo do usuário precisa ser definido.")]
        public Cargo Cargo { get; set; }

        [Required(ErrorMessage = "O e-mail precisa ser preenchido")]
        [EmailAddress(ErrorMessage = "Somente e-mails válidos são aceitos.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A data de nascimento do usuário precisa ser preenchida.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateOnly DataNascimento { get; set; }

        [Required(ErrorMessage = "O número de telefone do usuário precisa ser preenchido.")]
        [Display(Name = "Número de Telefone")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "O número de telefone precisa ser válido.")]
        public string NumeroTelefone { get; set; }

        public EnderecoViewModel? Endereco { get; set; }

        public IPagedList<Usuario>? Usuarios;

        public int? PaginaAtual;

    }
}
