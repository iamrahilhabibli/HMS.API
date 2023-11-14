using HMS.Domain.Entities.Common;

namespace HMS.Domain.Entities
{
    public class HotelPolicy:BaseEntity
    {
        public Hotel Hotel { get; set; } = null!;
        public TimeOnly CheckInTime { get; set; }
        public TimeOnly CheckOutTime { get; set; }
        public bool IsEarlyCheckInAllowed { get; set; }
        public bool IsLateCheckOutAllowed { get; set; }
        public bool IsSmokingAllowed { get; set; }
    }
}
