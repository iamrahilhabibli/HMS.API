using HMS.Application.DTOs.Hotel_DTOs;
using HMS.Application.Wrappers;

namespace HMS.Application.Abstraction.Services
{
    public interface IHotelService
    {
        Task CreateHotel(string appUserId, HotelCreateDto hotelCreateDto);
        List<HotelGetDto> GetAllHotels();
        Task<HotelGetDto> GetHotelById(Guid id);
        Task<PaginatedResult<HotelGetDto>> GetHotelsPaginated(int page = 1, int pageSize = 3);
        Task UpdateHotelById(Guid id, HotelUpdateDto hotelUpdateDto);
        Task DeleteHotel(Guid id);
    }
}
