using NotificationManagement.Entity;
using NotificationManagement.Model;

namespace NotificationManagement.Interfaces
{
    public interface INotificationService
    {
        Task<Result<List<Notification>>> GetNotifications();
        Task<Result<Notification>> Save(Notification organization);
        Task<Result<Notification>> Update(Notification organization);
        Task<Result<Notification>> Delete(long id);
        Task<Result<Notification>> Read(long id);
    }
}
