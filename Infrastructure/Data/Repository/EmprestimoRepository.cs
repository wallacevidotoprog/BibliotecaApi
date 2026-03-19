using BibliotecaApi.Domain.Entities;
using BibliotecaApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Infrastructure.Data.Repository
{
    public class EmprestimoRepository : IEmprestimoRepository
    {
        private readonly BibliotecaDbContext _context;

        public EmprestimoRepository(BibliotecaDbContext context)
        {
            _context = context;
        }

        public async Task<EmprestimoEntity?> GetByIdAsync(int id)
        {
            return await _context.Emprestimos
                .Include(e => e.Usuario)
                .Include(e => e.Livro)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<EmprestimoEntity>> GetAllAsync()
        {
            return await _context.Emprestimos
                .Include(e => e.Usuario)
                .Include(e => e.Livro)
                .ToListAsync();
        }

        public async Task AddAsync(EmprestimoEntity emprestimo)
        {
            await _context.Emprestimos.AddAsync(emprestimo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EmprestimoEntity emprestimo)
        {
            _context.Emprestimos.Update(emprestimo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var emprestimo = await _context.Emprestimos.FindAsync(id);
            if (emprestimo != null)
            {
                _context.Emprestimos.Remove(emprestimo);
                await _context.SaveChangesAsync();
            }
        }
    }
}
