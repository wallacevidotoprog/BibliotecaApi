using BibliotecaApi.Infrastructure.IOC;
using BibliotecaApi.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();


builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

// Executa o banco e o Seeder
DatabaseSeeder.Seed(app.Services);
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
