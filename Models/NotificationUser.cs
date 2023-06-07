using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace NotificationService.Models
{
    public class NotificationUser 
    {
        public int NotificationId { get; set; }
        public Notification Notification { get; set; }
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public bool IsRead { get; set; } = false;

       
    }


}
