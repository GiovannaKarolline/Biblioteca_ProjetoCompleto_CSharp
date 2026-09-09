using Microsoft.AspNetCore.Identity;

namespace Biblioteca.Models.Roles
{
    public class Visitante : IdentityRole<Guid>
    {
        public Visitante() : base("Visitante")
        {
            this.NormalizedName = this.Name.ToUpper();
        }
    }
}
