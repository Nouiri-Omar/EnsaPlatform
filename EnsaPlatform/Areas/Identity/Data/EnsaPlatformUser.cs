using Microsoft.AspNetCore.Identity;

namespace EnsaPlatform.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the EnsaPlatformUser class
    public class EnsaPlatformUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
