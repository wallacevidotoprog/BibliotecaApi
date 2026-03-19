using BibliotecaApi.Domain.Entities;
using BibliotecaApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Infrastructure.Data.Repository
{
    public class LivroRepository : ILivroRepository
    {
        private readonly BibliotecaDbContext _context;

        public LivroRepository(BibliotecaDbContext context)
        {
            _context = context;
        }

        public async Task<LivroEntity?> GetByIdAsync(int id)
        {
            return await _context.Livros.FindAsync(id);
        }

        public async Task<IEnumerable<LivroEntity>> GetAllAsync()
        {
            return await _context.Livros.ToListAsync();
        }

        public async Task AddAsync(LivroEntity livro)
        {
            await _context.Livros.AddAsync(livro);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LivroEntity livro)
        {
            _context.Livros.Update(livro);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var livro = await _context.Livros.FindAsync(id);
            if (livro != null)
            {
                _context.Livros.Remove(livro);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteIsbnAsync(string isbn)
        {
            return await _context.Livros.AnyAsync(l => l.ISBN.Valor == isbn);
        }

        public async Task<bool> ExisteIsbnExceptIdAsync(string isbn, int id)
        {
            return await _context.Livros.AnyAsync(l => l.ISBN.Valor == isbn && l.Id != id);
        }
    }
}
