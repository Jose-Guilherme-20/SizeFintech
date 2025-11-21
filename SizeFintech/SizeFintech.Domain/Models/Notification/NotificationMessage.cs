
namespace SizeFintech.Domain.Models.Notification
{
    public class NotificationMessage(string key, string value)
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Key { get; private set; } = key;
        public string Value { get; private set; } = value;


    }
}
