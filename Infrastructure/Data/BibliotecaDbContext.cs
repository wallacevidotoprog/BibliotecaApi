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

        public override int SaveChanges()
        {
            OnBeforeSaving();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is DefaultEntity trackable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.DataAtualizacao = DateTime.Now;
                            break;

                        case EntityState.Added:
                            trackable.DataCriacao = DateTime.Now;
                            break;
                    }
                }
            }
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

                entity.Property(e => e.Ativo);

                entity.OwnsOne(e => e.CPF, b =>
                {
                    b.Property(p => p.Numero).HasColumnName("CPF");
                });

                entity.OwnsOne(e => e.Email, b =>
                {
                    b.Property(p => p.Endereco).HasColumnName("Email");
                });
            });

            modelBuilder.Entity<LivroEntity>(entity =>
            {
                entity.ToTable("Livros");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Ativo).HasDefaultValue(true);

                entity.OwnsOne(e => e.ISBN, b =>
                {
                    b.Property(p => p.Valor).HasColumnName("ISBN");
                });
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
