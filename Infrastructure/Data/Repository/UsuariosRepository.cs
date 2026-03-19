using BibliotecaApi.Domain.Entities;
using BibliotecaApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Infrastructure.Data.Repository
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly BibliotecaDbContext _context;

        public UsuariosRepository(BibliotecaDbContext context)
        {
            _context = context;
        }

        public async Task<UsuariosEntity?> GetByIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<IEnumerable<UsuariosEntity>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task AddAsync(UsuariosEntity usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UsuariosEntity usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }
    }
}
