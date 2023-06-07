using Microsoft.AspNetCore.Identity;

namespace NotificationService.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<NotificationUser> NotificationUsers { get; set; }
    }
}
