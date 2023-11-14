using HMS.Domain.Entities.Common;

namespace HMS.Domain.Entities
{
    public class Amenity : BaseEntity
    { 
        public string Name { get; set; } = null!;
        public string ImagePath { get; set; } = null!;
    }
}
