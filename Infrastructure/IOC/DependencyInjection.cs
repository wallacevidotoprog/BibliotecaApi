using BibliotecaApi.Domain.Interfaces;
using BibliotecaApi.Infrastructure.Data;
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

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}
