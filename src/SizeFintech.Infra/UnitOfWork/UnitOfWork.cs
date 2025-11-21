
using Microsoft.EntityFrameworkCore;
using SizeFintech.Domain.Interfaces.UnitOfWork;
using SizeFintech.Infra.Context;

namespace SizeFintech.Infra.UnitOfWork
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        public async Task<int> CommitAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
