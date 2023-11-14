using HMS.Domain.Entities.Common;

namespace HMS.Domain.Entities
{
    public class Hotel : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Rating { get; set; }
        public int FloorNumber { get; set; }
        public int RoomNumber { get; set; }
        public ICollection<Amenity>? Amenities { get; set; } 
    }
}
