using AutoMapper;
using HMS.Application.Abstraction.Repositories.IHotelRepositories;
using HMS.Application.Abstraction.Services;
using HMS.Application.DTOs.Hotel_DTOs;
using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HMS.Persistence.Implementations.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelWriteRepository _hotelWriteRepository;
        private readonly IHotelReadRepository _hotelReadRepository;
        private readonly IMapper _mapper;

        public HotelService(IHotelWriteRepository hotelWriteRepository,
                            IMapper mapper,
                            IHotelReadRepository hotelReadRepository)
        {
            _hotelWriteRepository = hotelWriteRepository;
            _mapper = mapper;
            _hotelReadRepository = hotelReadRepository;
        }

        public async Task CreateHotel(HotelCreateDto hotelCreateDto)
        {
            if (hotelCreateDto is null) { throw new ArgumentNullException(); }
            Hotel newHotel = _mapper.Map<Hotel>(hotelCreateDto);
            await _hotelWriteRepository.AddAsync(newHotel);
            await _hotelWriteRepository.SaveChangeAsync();
        }

        public async Task<HotelGetDto> GetHotelById(Guid id)
        {
            if (id == Guid.Empty) { throw new ArgumentNullException(); }
            var hotel = await _hotelReadRepository.GetByIdAsync(id);
            if (hotel is not Hotel) { throw new Exception(); }
            HotelGetDto hotelGetDto = _mapper.Map<HotelGetDto>(hotel);
            return hotelGetDto; 
        }

        public List<HotelGetDto> GetAllHotels()
        {
            var hotelsList =  _hotelReadRepository.GetAll();
            List<HotelGetDto> hotelGetDtos = _mapper.Map<List<HotelGetDto>>(hotelsList);
            return hotelGetDtos;

            // Manual Mapping

            //List<HotelGetDto> hotelGetDtos = new List<HotelGetDto>();
            //foreach (var hotel in hotelsList)
            //{
            //    HotelGetDto hotelGetDto = new HotelGetDto(
            //        hotel.Name,
            //        hotel.Description,
            //        hotel.Rating);

            //    hotelGetDtos.Add(hotelGetDto);  
            //}
            //return hotelGetDtos; 
       }

        public async Task<List<HotelGetDto>> GetHotelsPaginated(int page = 1, int pageSize = 3)
        {
            var hotels = await _hotelReadRepository.GetAllByExpressionOrderBy
                (hotel => hotel.IsDeleted == false, 
                pageSize, 
                (page - 1) * pageSize, 
                hotel => hotel.DateCreated)
                .ToListAsync();
            var hotelDtos = _mapper.Map<List<HotelGetDto>>(hotels);
            return hotelDtos;
        }
    }
}
