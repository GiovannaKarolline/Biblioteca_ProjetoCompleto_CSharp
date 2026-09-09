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

            _modelBuilder.Entity<Categoria>().HasData(
                new Categoria
                {
                    Id = Guid.Parse("8917733f-fe33-4f30-a0da-b96f13604a11"),
                    Titulo = "Terror"
                }
            );

            _modelBuilder.Entity<Editora>().HasData(
                new Editora
                {
                    Id = Guid.Parse("b366f588-1a07-4c76-9637-48c9a718a2b4"),
                    Nome = "Saraiva"
                }
            );

            _modelBuilder.Entity<ObraLiteraria>().HasData(
                new ObraLiteraria
                {
                    Id = Guid.Parse("4ff24196-23a0-4a3e-bf02-19c696de6d72"),
                    AnoPublicacao = 2015,
                    CategoriaId = Guid.Parse("8917733f-fe33-4f30-a0da-b96f13604a11"),
                    EditoraId = Guid.Parse("b366f588-1a07-4c76-9637-48c9a718a2b4"),
                    FotoCapa = "https://static.skeelo.com/remote/320/480/100/https://skoob.s3.amazonaws.com/livros/122469134/APRENDA_INGLES_SOZINHO_COM_CON_1719414152122469134SK-V11719414153B.jpg",
                    ISBN = "B09KNNYS6M",
                    Titulo = "Aprenda Inglês Sozinho Com Contos de Terror"
                }
            );
        }
    }
}
