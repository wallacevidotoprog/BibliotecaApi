using BibliotecaApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Infrastructure.Data
{
    public class BibliotecaDbContext : DbContext
    {
        public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options)
            : base(options)
        {
        }

        public DbSet<UsuariosEntity> Usuarios { get; set; }
        public DbSet<LivroEntity> Livros { get; set; }
        public DbSet<EmprestimoEntity> Emprestimos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UsuariosEntity>(entity =>
            {
                entity.ToTable("Usuarios");
                entity.HasKey(e => e.Id);

                entity.OwnsOne(e => e.CPF);
                entity.OwnsOne(e => e.Email);
            });

            modelBuilder.Entity<LivroEntity>(entity =>
            {
                entity.ToTable("Livros");
                entity.HasKey(e => e.Id);

                entity.OwnsOne(e => e.ISBN);
            });

            modelBuilder.Entity<EmprestimoEntity>(entity =>
            {
                entity.ToTable("Emprestimos");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Valor).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Multa).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Total).HasColumnType("decimal(18,2)");
                entity.HasOne(e => e.Usuario)
                      .WithMany()
                      .HasForeignKey(e => e.IdUsuario)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Livro)
                      .WithMany()
                      .HasForeignKey(e => e.IdLivro)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
