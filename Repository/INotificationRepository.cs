using NotificationService.Models;

namespace NotificationService.Repository
{
    public interface INotificationRepository
    {
        List<NotificationUser> GetUserNotifications(string userId);
        void Create(Notification notification, int petId);
        void ReadNotification(int notificationId, string userId);
    }
}
