using HMS.Domain.Entities.Common;

namespace HMS.Domain.Entities
{
    public class HotelPolicy:BaseEntity
    {
        public Hotel Hotel { get; set; } = null!;
        public Guid HotelId { get; set; }
        public string CheckInTime { get; set; } = null!;
        public string CheckOutTime { get; set; } = null!;
        public bool IsEarlyCheckInAllowed { get; set; }
        public bool IsLateCheckOutAllowed { get; set; }
        public bool IsSmokingAllowed { get; set; }
    }
}
