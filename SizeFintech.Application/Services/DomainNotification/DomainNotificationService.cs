
using SizeFintech.Domain.Interfaces.Notification;
using SizeFintech.Domain.Models.Notification;

namespace SizeFintech.Application.Services.DomainNotification
{
    public class DomainNotificationService : IDomainNotificationService
    {
        private readonly List<DomainNotificationModel> _notifications;

        public DomainNotificationService()
        {
            _notifications = new List<DomainNotificationModel>();
        }

        public bool HasNotifications() => _notifications.Any();

        public List<DomainNotificationModel> GetNotifications() => _notifications;

        public void AddNotification(string key, string message)
        {
            _notifications.Add(new DomainNotificationModel(key, message));
        }
    }
}
