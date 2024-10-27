using Microsoft.AspNetCore.Identity;

namespace Agas1.Logic.Models
{
    public class TankProcess
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ApplicationUser : IdentityUser
    {
        // Add additional properties if needed
    }
}
