using Microsoft.AspNetCore.SignalR;
using NotificationService.Infrastucture;
using NotificationService.Models;
using NotificationService.Persistence;

namespace NotificationService.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        public NotificationDbContext _context { get; }
     

        private IHubContext<SignalServer> _hubContext;

        public NotificationRepository(NotificationDbContext context,
                                        IHubContext<SignalServer> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public void Create(Notification notification, int petId)
        {
            _context.Notifications.Add(notification);
            _context.SaveChanges();

            //TODO: Assign notification to users
            var watchlists = _watchlistRepository.GetWatchlistFromPetId(petId);
            foreach (var watchlist in watchlists)
            {
                var userNotification = new NotificationApplicationUser();
                userNotification.ApplicationUserId = watchlist.UserId;
                userNotification.NotificationId = notification.Id;

                _context.UserNotifications.Add(userNotification);
                _context.SaveChanges();
            }

            _hubContext.Clients.All.InvokeAsync("displayNotification", "");
        }

        public List<NotificationApplicationUser> GetUserNotifications(string userId)
        {
            return _context.UserNotifications.Where(u => u.ApplicationUserId.Equals(userId) && !u.IsRead)
                                            .Include(n => n.Notification)
                                            .ToList();
        }

        public void ReadNotification(int notificationId, string userId)
        {
            var notification = _context.UserNotifications
                                        .FirstOrDefault(n => n.ApplicationUserId.Equals(userId)
                                        && n.NotificationId == notificationId);
            notification.IsRead = true;
            _context.UserNotifications.Update(notification);
            _context.SaveChanges();
        }

        List<NotificationUser> INotificationRepository.GetUserNotifications(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
}
