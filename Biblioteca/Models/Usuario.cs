using Biblioteca.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Models
{
    [Table("Usuarios")]
    public class Usuario : IdentityUser<Guid>
    {
        [Required]
        [StringLength(50)]
        public override string? UserName => base.UserName;

        [EmailAddress]
        [Required]
        [StringLength(255)]
        public override string? Email => base.Email; //o mesmo que pedir o valor de Email da classe base e preencher a propriedade

        [Required]
        [StringLength(100)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [DataType(DataType.PhoneNumber)]
        public override string? PhoneNumber => base.PhoneNumber;

        [Required]
        public Cargo Cargo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly DataNascimento { get; set; }

        public bool Deletado { get; set; } = false;

        public List<Emprestimo>? Emprestimos { get; set; }

        public override string ToString()
        {
            return $"\nUsuário:\n" +
                $"\n* Id: {Id.ToString()}" +
                $"\n* Nome de usuário: {UserName}" +
                $"\n* E-mail: {Email}" +
                $"\n* Senha: {Senha}" +
                $"\n* Número de Telefone: {PhoneNumber}" +
                $"\n* Cargo: {Cargo.ToString()}" +
                $"\n* Data de Nascimento: {DataNascimento.ToString()}\n";
        }
    }
}
