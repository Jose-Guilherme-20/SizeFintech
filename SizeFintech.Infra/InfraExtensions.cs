
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SizeFintech.Infra
{
    public static class InfraExtensions
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Context.AppDbContext>( db => db.UseSqlServer(configuration.GetConnectionString("Database")));

            services.AddScoped<Domain.Interfaces.UnitOfWork.IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddScoped(typeof(Domain.Interfaces.Repository.IRepositoryBase<>), typeof(Repository.EntityBaseRepository<>));
            return services;
        }
    }
}
