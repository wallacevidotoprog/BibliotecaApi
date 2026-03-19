using BibliotecaApi.Domain.Entities;

namespace BibliotecaApi.Domain.Interfaces
{
    public interface IUsuariosRepository
    {
        Task<UsuariosEntity?> GetByIdAsync(int id);
        Task<IEnumerable<UsuariosEntity>> GetAllAsync();
        Task AddAsync(UsuariosEntity usuario);
        Task UpdateAsync(UsuariosEntity usuario);
        Task DeleteAsync(int id);
    }
}
