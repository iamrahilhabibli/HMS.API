namespace HMS.Application.DTOs.Hotel_DTOs
{
    public record HotelCreateDto(string Name, string Description, int FloorNumber, int RoomNumber, string CheckInTime, string CheckOutTime, bool IsEarlyCheckInAllowed, bool IsLateCheckOutAllowed, bool IsSmokingAllowed = false);
}
