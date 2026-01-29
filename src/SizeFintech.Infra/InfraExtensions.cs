
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
            services.AddDbContext<Context.AppDbContext>((serviceProvider, options) =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Database"));

            });
            #endregion

            #region UnitOfWork
            services.AddScoped<Domain.Interfaces.UnitOfWork.IUnitOfWork, UnitOfWork.UnitOfWork>();
            #endregion

            #region Repositories
            services.AddScoped(typeof(Domain.Interfaces.Repository.IEntityBaseRepository<>), typeof(Repository.EntityBaseRepository<>));
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            #endregion

            return services;
        }
    }
}
