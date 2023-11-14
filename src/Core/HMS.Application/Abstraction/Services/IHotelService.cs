using HMS.Application.DTOs.Hotel_DTOs;

namespace HMS.Application.Abstraction.Services
{
    public interface IHotelService
    {
        Task CreateHotel(HotelCreateDto hotelCreateDto);
    }
}
