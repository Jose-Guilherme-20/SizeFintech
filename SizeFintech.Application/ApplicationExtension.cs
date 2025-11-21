
using Microsoft.Extensions.DependencyInjection;
using SizeFintech.Application.UseCases.Company.GetCompanyByCnpj;
using SizeFintech.Application.UseCases.Company.InsertCompany;
using SizeFintech.Application.UseCases.Company.UpdateCompany;
using SizeFintech.Application.UseCases.Invoice.InsertInvoice;
using SizeFintech.Domain.Interfaces.Notification;
using SizeFintech.Domain.Models.Notification;

namespace SizeFintech.Application
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            #region UseCases
            services.AddScoped<IGetCompanyByCnpjUseCase, GetCompanyByCnpjUseCase>();
            services.AddScoped<IInsertCompanyUseCase, InsertCompanyUseCase>();
            services.AddScoped<IUpdateCompanyUseCase, UpdateCompanyUseCase>();
            services.AddScoped<IInsertInvoiceUseCase, InsertInvoiceUseCase>();
            #endregion

            #region Services
            services.AddScoped<IDomainNotification, DomainNotification>();
            #endregion

            return services;
        }


    }
}
