
using SizeFintech.Domain.Models.Entities;

namespace SizeFintech.Domain.Interfaces.Repository
{
    public interface IEntityBaseRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
