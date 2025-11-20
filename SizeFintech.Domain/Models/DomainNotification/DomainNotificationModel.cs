
namespace SizeFintech.Domain.Models.Notification
{
    public class DomainNotificationModel(string key, string message)
    {
        public string Key { get; } = key;
        public string Message { get; } = message;


    }
}
