namespace NotificationService.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<NotificationUser> NotificationUsers { get; set; }
    }
}
