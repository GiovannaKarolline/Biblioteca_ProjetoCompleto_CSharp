using Biblioteca.Areas.Administration.Services;
using Biblioteca.Areas.Administration.Services.Interfaces;
using Biblioteca.Context;
using Biblioteca.Models;
using Biblioteca.Repositories;
using Biblioteca.Repositories.Interfaces;
using Biblioteca.Services;
using Biblioteca.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<BibliotecaDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
);

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICopiaRepository, CopiaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IObraLiterariaRepository, ObraLiterariaRepository>();
builder.Services.AddScoped<IAutorRepository, AutorRepository>();
builder.Services.AddScoped<IEditoraRepository, EditoraRepository>();
builder.Services.AddScoped<IEmprestimoRepository, EmprestimoRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IObraLiterariaService, ObraLiterariaService>();
builder.Services.AddScoped<IAutorService, AutorService>();
//builder.Services.AddScoped<ICategoriaService, CategoriaService>();
//builder.Services.AddScoped<IEditoraService, EditoraService>();
builder.Services.AddScoped<IEmprestimoService, EmprestimoService>();
builder.Services.AddScoped<ICopiaService, CopiaService>();

builder.Services.AddSession();
builder.Services.AddIdentity<Usuario, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<BibliotecaDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Views/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthorization();
app.UseAuthorization();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//        name:"default",
//        pattern:"{controller=Home}/{action=Index}/{id?}"
//    );
//});

app.MapControllerRoute(
    name: "administration",
    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
