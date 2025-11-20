
using SizeFintech.Domain.Models.Notification;

namespace SizeFintech.Domain.Interfaces.Notification
{
    public interface IDomainNotificationService
    {
        bool HasNotifications();
        List<DomainNotificationModel> GetNotifications();
        void AddNotification(string key, string message);
    }
}
