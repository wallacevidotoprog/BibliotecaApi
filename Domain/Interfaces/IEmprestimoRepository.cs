using BibliotecaApi.Domain.Entities;

namespace BibliotecaApi.Domain.Interfaces
{
    public interface IEmprestimoRepository
    {
        Task<EmprestimoEntity?> GetByIdAsync(int id);
        Task<IEnumerable<EmprestimoEntity>> GetAllAsync();
        Task AddAsync(EmprestimoEntity emprestimo);
        Task UpdateAsync(EmprestimoEntity emprestimo);
        Task DeleteAsync(int id);
        Task<IEnumerable<EmprestimoEntity>> GetByUsuarioIdAsync(int usuarioId);
    }
}
