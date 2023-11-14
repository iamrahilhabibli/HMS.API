using AutoMapper;
using HMS.Application.Abstraction.Repositories.IHotelRepositories;
using HMS.Application.Abstraction.Services;
using HMS.Application.DTOs.Hotel_DTOs;
using HMS.Domain.Entities;

namespace HMS.Persistence.Implementations.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelWriteRepository _hotelWriteRepository;
        private readonly IMapper _mapper;

        public HotelService(IHotelWriteRepository hotelWriteRepository,
                            IMapper mapper)
        {
            _hotelWriteRepository = hotelWriteRepository;
            _mapper = mapper;
        }

        public async Task CreateHotel(HotelCreateDto hotelCreateDto)
        {
            if (hotelCreateDto is null) { throw new ArgumentNullException(); }
            Hotel newHotel = _mapper.Map<Hotel>(hotelCreateDto);
            await _hotelWriteRepository.AddAsync(newHotel);
            await _hotelWriteRepository.SaveChangeAsync();
        }
    }
}
