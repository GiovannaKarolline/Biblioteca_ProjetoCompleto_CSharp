using Biblioteca.Models;
using Biblioteca.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data.Common;

namespace Biblioteca.Context
{
    public class BibliotecaDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<ObraLiteraria> ObrasLiterarias { get; set; }
        public DbSet<Copia> Copias { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }
        public DbSet<Editora> Editoras { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Autor>()
                .HasMany(autor => autor.ObrasLiterarias)
                .WithMany(obra => obra.Autores);

            modelBuilder.Entity<Copia>()
                .HasMany(copia => copia.Emprestimos)
                .WithMany(emprestimo => emprestimo.Copias);

            modelBuilder.Entity<Copia>()
                .HasOne(copia => copia.ObraLiteraria)
                .WithMany(obra => obra.Copias)
                .HasForeignKey(copia => copia.ObraId);

            modelBuilder.Entity<ObraLiteraria>()
                .HasOne(obra => obra.Categoria)
                .WithMany(categoria => categoria.ObrasLiterarias)
                .HasForeignKey(obra => obra.CategoriaId);

            modelBuilder.Entity<ObraLiteraria>()
                .HasOne(obra => obra.Editora)
                .WithMany(editora => editora.Obras)
                .HasForeignKey(obra => obra.EditoraId);

            modelBuilder.Entity<Emprestimo>()
                .HasOne(emprestimo => emprestimo.Usuario)
                .WithMany(usuario => usuario.Emprestimos)
                .HasForeignKey(emprestimo => emprestimo.UsuarioId);

            modelBuilder.Entity<Usuario>()
                .HasIndex(coluna => coluna.Email)
                .IsUnique();

            modelBuilder.Entity<Emprestimo>()
                .HasKey(emprestimo => emprestimo.Id);

            modelBuilder.Entity<Categoria>()
                .HasKey(categoria => categoria.Id);

            modelBuilder.Entity<ObraLiteraria>()
                .HasKey(obraLiteraria => obraLiteraria.Id);

            modelBuilder.Entity<Copia>()
                .HasKey(copia => copia.Id);

            modelBuilder.Entity<Autor>()
                .HasKey(autor => autor.Id);

            modelBuilder.Entity<Editora>()
                .HasKey(editora => editora.Id);

            RolesSeed rolesPadrao = new RolesSeed(modelBuilder);

            rolesPadrao.AdicionarRolesPadrao();
        }
    }
}
