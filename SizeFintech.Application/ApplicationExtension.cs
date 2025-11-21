
using Microsoft.Extensions.DependencyInjection;
using SizeFintech.Application.Services.DomainNotification;
using SizeFintech.Application.UseCases.Recebivel.CadastroEmpresa;
using SizeFintech.Domain.Interfaces.Notification;

namespace SizeFintech.Application
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            #region UseCases
            services.AddScoped<ICadastrarEmpresaUseCase, CadastrarEmpresaUseCase>();
            #endregion

            #region Services
            services.AddScoped<IDomainNotification, DomainNotification>();
            #endregion

            return services;
        }


    }
}
