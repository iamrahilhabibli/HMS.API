using HMS.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace HMS.Domain.Identity
{
    public class AppUser : IdentityUser
    {
        public Visitor? Visitor { get; set; }
        public HotelManager? Manager { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiration { get; set; }
    }
}
