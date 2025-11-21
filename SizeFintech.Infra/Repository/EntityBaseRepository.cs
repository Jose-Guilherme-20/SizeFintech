
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SizeFintech.Domain.Interfaces.Repository;
using SizeFintech.Domain.Models.Entities;
using SizeFintech.Infra.Context;

namespace SizeFintech.Infra.Repository
{
    public class EntityBaseRepository<T>(AppDbContext context) : IEntityBaseRepository<T> where T : BaseEntity
    {

        public async Task AddAsync(T entity)
        {
            await context.AddAsync(entity);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id); 
        }

        public virtual async Task<T?> GetByParamsAsync(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().FirstOrDefaultAsync(predicate);
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
