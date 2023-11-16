using HMS.Application.DTOs.Hotel_DTOs;

namespace HMS.Application.Abstraction.Services
{
    public interface IHotelService
    {
        Task CreateHotel(HotelCreateDto hotelCreateDto);
        List<HotelGetDto> GetAllHotels();
        Task<HotelGetDto> GetHotelById(Guid id);
        Task<List<HotelGetDto>> GetHotelsPaginated(int page = 1, int pageSize = 3);
    }
}
