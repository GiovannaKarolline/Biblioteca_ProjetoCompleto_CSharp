using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

namespace Biblioteca.Models.Roles
{
    public class Administrador : IdentityRole<Guid>
    {
        public Administrador() : base("Administrador") 
        {
            this.NormalizedName = this.Name.ToUpper();
        }
    }
}
