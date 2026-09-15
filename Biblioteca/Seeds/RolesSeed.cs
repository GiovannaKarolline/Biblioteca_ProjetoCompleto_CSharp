using Biblioteca.Models;
using Biblioteca.Models.Roles;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Seeds
{
    public class RolesSeed
    {
        private readonly ModelBuilder _modelBuilder;
        public RolesSeed(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public void AdicionarRolesPadrao()
        {
            _modelBuilder.Entity<Administrador>().HasData(
                new Administrador { Id = Guid.Parse("a75fe880-f314-4a02-8109-21931ba9e0b7")});

            _modelBuilder.Entity<Visitante>().HasData(
                new Visitante { Id = Guid.Parse("3f027812-847d-4b0b-81dd-68bcbf70dd8a")});

        }
    }
}
