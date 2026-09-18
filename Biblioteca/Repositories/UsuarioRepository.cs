using Biblioteca.Context;
using Biblioteca.Models;
using Biblioteca.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly BibliotecaDbContext _context;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly IHttpContextAccessor _httpContext;

        public UsuarioRepository(BibliotecaDbContext context, SignInManager<Usuario> signInManager, UserManager<Usuario> userManager, IHttpContextAccessor httpContext)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _httpContext = httpContext;
        }

        public async Task<Usuario?> GetUsuarioById(Guid id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(usuario => usuario.Id == id);
        }

        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosByNome(string nome)
        {
            return await _context.Usuarios.Where(usuario => usuario.NormalizedUserName.Contains(nome.ToUpper())).ToListAsync();
        }

        public async Task<Usuario?> CriarUsuario(Usuario usuario)
        {
            var resultadoCriacao = await _userManager.CreateAsync(usuario, usuario.Senha);

            if (resultadoCriacao.Succeeded)
            {
                await _userManager.AddToRoleAsync(usuario, usuario.Cargo.ToString());
                await _context.SaveChangesAsync();
            }
            return await GetUsuarioById(usuario.Id);
        }

        public async Task<Usuario?> LogarUsuario(Usuario usuario)
        {
            if (GetUsuarioById(usuario.Id) != null)
            {
                var resultado = await _signInManager.PasswordSignInAsync(usuario, usuario.Senha, true, false);

                if (!resultado.Succeeded)
                {
                    throw new ArgumentException("Falha ao logar usuário.");
                }
            }
            else
            {
                throw new ArgumentException("Falha ao logar usuário: usuário não cadastrado.");
            }

            return usuario;
        }

        public async Task DeslogarUsuario()
        {
            _httpContext.HttpContext.Session.Clear();
            _httpContext.HttpContext.User = null;
            await _signInManager.SignOutAsync();
        }

        public Task<Usuario> DeletarUsuario(Usuario usuario)
        {
            AtualizarUsuario(usuario);

            return Task.FromResult(usuario);
        }

        public Task<Usuario> AtualizarUsuario(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();

            return Task.FromResult(usuario);
        }
    }
}