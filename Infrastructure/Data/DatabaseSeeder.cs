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

            // Cria o diretório do banco se não existir
            var dbPath = "Database";
            if (!Directory.Exists(dbPath))
            {
                Directory.CreateDirectory(dbPath);
            }

            // Garante que o banco seja criado e execute as criações de tabelas
            context.Database.EnsureCreated();

            // Verifica se já existe algum usuário
            if (!context.Usuarios.Any())
            {
                var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

                var admin = new UsuariosEntity();
                // var senhaHash = passwordHasher.HashPassword("admin123");
                admin.Cadastrar("Administrador", "00000000000", "admin@biblioteca.com", "admin123");

                context.Usuarios.Add(admin);
                context.SaveChanges();
            }
        }
    }
}
