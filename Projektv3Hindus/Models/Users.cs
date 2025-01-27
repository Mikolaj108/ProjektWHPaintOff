using Microsoft.AspNetCore.Identity;

namespace WarhammerPaintCenter.Models
{
    public class Users : IdentityUser
    {
        public string FullName {get; set;}
    }
}
