using HMS.Application.Abstraction.Services;
using HMS.Application.DTOs.Hotel_DTOs;
using HMS.Application.Wrappers;
using Microsoft.Extensions.Caching.Memory;

namespace HMS.Persistence.Implementations.CachedServices
{
    public class CachedHotelService : IHotelService
    {
        private const string HotelListCachedKey = "HotelList";
        private readonly IMemoryCache _memoryCache;
        private readonly IHotelService _hotelService;

        public CachedHotelService(IMemoryCache memoryCache,
                                  IHotelService hotelService)
        {
            _memoryCache = memoryCache;
            _hotelService = hotelService;
        }

        public Task CreateHotel(HotelCreateDto hotelCreateDto)
        {
            throw new NotImplementedException();
        }

        public List<HotelGetDto> GetAllHotels()
        {
            throw new NotImplementedException();
        }

        public Task<HotelGetDto> GetHotelById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginatedResult<HotelGetDto>> GetHotelsPaginated(int page = 1, int pageSize = 3)
        {
            var options = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(10))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(30));
            if (_memoryCache.TryGetValue(HotelListCachedKey, out Task<PaginatedResult<HotelGetDto>> result)) 
            return await result;
            result = _hotelService.GetHotelsPaginated(page, pageSize);
            await _memoryCache.Set(HotelListCachedKey, result,options);
            return await result;
        }
    }
}
