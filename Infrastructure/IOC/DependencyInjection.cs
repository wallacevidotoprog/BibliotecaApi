using BibliotecaApi.Application.UseCases.Emprestimos;
using BibliotecaApi.Application.UseCases.Livros;
using BibliotecaApi.Application.UseCases.Usuarios;
using BibliotecaApi.Domain.Interfaces;
using BibliotecaApi.Infrastructure.Data;
using BibliotecaApi.Infrastructure.Data.Repository;
using BibliotecaApi.Infrastructure.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Infrastructure.IOC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BibliotecaDbContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IPasswordHasher, PasswordHasher>();

            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<IUsuariosRepository, UsuariosRepository>();
            services.AddScoped<IEmprestimoRepository, EmprestimoRepository>();

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Livros
            services.AddScoped<CriarLivroUseCase>();
            services.AddScoped<AtualizarLivroUseCase>();
            services.AddScoped<ObterLivroUseCase>();
            services.AddScoped<ExcluirLivroUseCase>();
            services.AddScoped<AlternarStatusLivroUseCase>();

            // Usuarios
            services.AddScoped<CriarUsuarioUseCase>();
            services.AddScoped<AtualizarUsuarioUseCase>();
            services.AddScoped<ObterUsuarioUseCase>();
            services.AddScoped<ExcluirUsuarioUseCase>();
            services.AddScoped<AlternarStatusUsuarioUseCase>();

            // Emprestimos
            services.AddScoped<CriarEmprestimoUseCase>();
            services.AddScoped<RegistrarDevolucaoUseCase>();
            services.AddScoped<ObterEmprestimoUseCase>();
            services.AddScoped<ExcluirEmprestimoUseCase>();

            return services;
        }
    }
}
