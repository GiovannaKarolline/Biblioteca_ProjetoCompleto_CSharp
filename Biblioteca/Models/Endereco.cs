using Biblioteca.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Models
{
    [Table("Enderecos")]
    public class Endereco
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Logradouro { get; set; }

        [StringLength(8)]
        public string Numero { get; set; }

        [Required]
        [StringLength(8)]
        public string Cep { get; set; }

        [StringLength(50)]
        public string? Complemento { get; set; }

        public Usuario Usuario { get; set; }

        [Required]
        public TipoLogradouro TipoLogradouro { get; set; }

        public bool Deletado { get; set; } = false;

        [Required]
        public Guid UsuarioId { get; set; }

        public override string ToString()
        {
            return $"\nEndereço" +
                $"\n* Id: {Id}" +
                $"\n* Id do Usuário: {UsuarioId}" +
                $"\n* Logradouro: {TipoLogradouro.ToString() + " " +Logradouro}" +
                $"\n* Número: {Numero}" +
                $"\n* CEP: {Cep}" +
                $"\n* Complemento: {Complemento}\n";
        }
    }
}
