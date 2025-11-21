
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SizeFintech.Domain.Interfaces.Repository;
using SizeFintech.Infra.Repository;

namespace SizeFintech.Infra
{
    public static class InfraExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region DbContext
            services.AddDbContext<Context.AppDbContext>( db => db.UseSqlServer(configuration.GetConnectionString("Database")));
            #endregion

            #region UnitOfWork
            services.AddScoped<Domain.Interfaces.UnitOfWork.IUnitOfWork, UnitOfWork.UnitOfWork>();
            #endregion

            #region Repositories
            services.AddScoped(typeof(Domain.Interfaces.Repository.IRepositoryBase<>), typeof(Repository.EntityBaseRepository<>));
            services.AddScoped<IEmpresaRepository, EmpresaRepository>();
            services.AddScoped<ICarrinhoRepository, CarrinhoRepository>();
            services.AddScoped<INotaFiscalRepository, NotaFiscalRepository>();
            #endregion

            return services;
        }
    }
}
