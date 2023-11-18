using HMS.Application.Abstraction.Services;
using HMS.Application.DTOs.Hotel_DTOs;
using HMS.Application.Wrappers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace HMS.Persistence.Implementations.CachedServices
{
    public class CachedHotelServiceDecorator : ICachedHotelService
    {
        private const string HotelListCachedKey = "HotelList";
        private readonly ILogger<CachedHotelServiceDecorator> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly IHotelService _hotelService;

        public CachedHotelServiceDecorator(IMemoryCache memoryCache,
                                  IHotelService hotelService,
                                  ILogger<CachedHotelServiceDecorator> logger)
        {
            _memoryCache = memoryCache;
            _hotelService = hotelService;
            _logger = logger;
        }

        public async Task CreateHotel(HotelCreateDto hotelCreateDto)
        {
            await _hotelService.CreateHotel(hotelCreateDto);
        }

        public List<HotelGetDto> GetAllHotels()
        {
            var options = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(10))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(30));

            if(!_memoryCache.TryGetValue(HotelListCachedKey, out List<HotelGetDto> result))
            {
                result = _hotelService.GetAllHotels();
                _memoryCache.Set(HotelListCachedKey, result,options);
                _logger.LogInformation("Hotels Caching Done");
            }
            return result;  
        }

        public async Task<HotelGetDto> GetHotelById(Guid id)
        {
            return await _hotelService.GetHotelById(id);
        }
        public async Task<PaginatedResult<HotelGetDto>> GetHotelsPaginated(int page = 1, int pageSize = 3)
        {
            return await _hotelService.GetHotelsPaginated(page, pageSize);  
        }
    }
}
