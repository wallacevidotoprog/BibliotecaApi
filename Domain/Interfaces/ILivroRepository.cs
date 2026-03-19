using BibliotecaApi.Domain.Entities;

namespace BibliotecaApi.Domain.Interfaces
{
    public interface ILivroRepository
    {
        Task<LivroEntity?> GetByIdAsync(int id);
        Task<IEnumerable<LivroEntity>> GetAllAsync();
        Task AddAsync(LivroEntity livro);
        Task UpdateAsync(LivroEntity livro);
        Task DeleteAsync(int id);
    }
}
