using System.Threading.Tasks;

namespace QHomeGroup.Application.Notification
{
    public interface INotificationService
    {
        Task<bool> SendToDevice(string token, string title, string body, string contactId);
    }
}
