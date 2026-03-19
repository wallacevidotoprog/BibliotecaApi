using BibliotecaApi.Domain.Entities;
using BibliotecaApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BibliotecaApi.Infrastructure.Data
{
    public static class DatabaseSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<BibliotecaDbContext>();

            var dbPath = "Database";
            if (!Directory.Exists(dbPath))
            {
                Directory.CreateDirectory(dbPath);
            }

            context.Database.EnsureCreated();

            if (!context.Usuarios.Any())
            {
                var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

                var admin = new UsuariosEntity();
                // var senhaHash = passwordHasher.HashPassword("admin123");
                admin.Cadastrar("Administrador", "00000000000", "admin@biblioteca.com", "admin123");

                context.Usuarios.Add(admin);
                context.SaveChanges();
            }

            if (!context.Livros.Any())
            {
                var livros = new List<LivroEntity>();
                
                var l1 = new LivroEntity(); l1.Cadastrar(null, "Clean Code", "Robert C. Martin", "9780132350884"); l1.Ativar();
                var l2 = new LivroEntity(); l2.Cadastrar(null, "Clean Architecture", "Robert C. Martin", "9780134494166"); l2.Ativar();
                var l3 = new LivroEntity(); l3.Cadastrar(null, "The Pragmatic Programmer", "Andrew Hunt", "9780135957059"); l3.Ativar();
                var l4 = new LivroEntity(); l4.Cadastrar(null, "Refactoring", "Martin Fowler", "9780134757599"); l4.Ativar();
                var l5 = new LivroEntity(); l5.Cadastrar(null, "The Clean Coder", "Robert C. Martin", "9780137081073"); l5.Ativar();
                var l6 = new LivroEntity(); l6.Cadastrar(null, "Head First Design Patterns", "Eric Freeman", "9780596007126"); l6.Ativar();
                var l7 = new LivroEntity(); l7.Cadastrar(null, "Design Patterns", "Erich Gamma", "9780201633610"); l7.Ativar();
                var l8 = new LivroEntity(); l8.Cadastrar(null, "Dependency Injection", "Steven van Deursen", "9781617294549"); l8.Ativar();
                var l9 = new LivroEntity(); l9.Cadastrar(null, "Domain-Driven Design", "Eric Evans", "9780132149181"); l9.Ativar();
                var l10 = new LivroEntity(); l10.Cadastrar(null, "Code Complete", "Steve McConnell", "9780735619678"); l10.Ativar();

                livros.AddRange(new[] { l1, l2, l3, l4, l5, l6, l7, l8, l9, l10 });

                context.Livros.AddRange(livros);
                context.SaveChanges();
            }
        }
    }
}
