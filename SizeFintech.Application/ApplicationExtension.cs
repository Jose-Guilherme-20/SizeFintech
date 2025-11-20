
using Microsoft.Extensions.DependencyInjection;
using SizeFintech.Application.Services.DomainNotification;
using SizeFintech.Domain.Interfaces.Notification;

namespace SizeFintech.Application
{
    public static class ApplicationExtension
    {
        public static ServiceCollection AddApplication(this ServiceCollection services)
        {
            
            
            services.AddScoped<IDomainNotificationService, DomainNotificationService>();

            return services;
        }
    }
}
