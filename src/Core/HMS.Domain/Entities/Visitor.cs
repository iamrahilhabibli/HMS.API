using HMS.Domain.Entities.Common;
using HMS.Domain.Identity;

namespace HMS.Domain.Entities
{
    public class Visitor : BaseEntity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public AppUser AppUser { get; set; } = null!;
        public string AppUserId { get; set; } = null!;
    }
}
