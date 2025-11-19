
using Microsoft.EntityFrameworkCore;
using SizeFintech.Domain.Interfaces.Repository;
using SizeFintech.Domain.Models.Entities;
using SizeFintech.Infra.Context;

namespace SizeFintech.Infra.Repository
{
    public class EntityBaseRepository<T>(AppDbContext context) : IRepositoryBase<T> where T : BaseEntity
    {

        public async Task AddAsync(T entity)
        {
            await context.AddAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id); 
        }

        public void Remove(T entity)
        {
            context.Remove(entity);
        }

        public void Update(T entity)
        {
            context.Update(entity);
        }
    }
}
