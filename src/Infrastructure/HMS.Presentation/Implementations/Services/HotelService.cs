using AutoMapper;
using HMS.Application.Abstraction.Repositories.IHotelRepositories;
using HMS.Application.Abstraction.Services;
using HMS.Application.DTOs.Hotel_DTOs;
using HMS.Application.Wrappers;
using HMS.Domain.Entities;
using HMS.Persistence.Exceptions;
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

        private bool HotelExists(HotelCreateDto hotelCreateDto)
        {
            var hotel = _hotelReadRepository.GetByExpressionAsync(hotel => hotel.Name == hotelCreateDto.Name);
            if (hotel is null) { return false; }
            return true;
        }
        public async Task CreateHotel(HotelCreateDto hotelCreateDto)
        {
            if (hotelCreateDto is null) { throw new ArgumentNullException(); }
            var hotelExists = HotelExists(hotelCreateDto);
            if (hotelExists)
            {
                Hotel newHotel = _mapper.Map<Hotel>(hotelCreateDto);
                await _hotelWriteRepository.AddAsync(newHotel);
                await _hotelWriteRepository.SaveChangeAsync();
                return;
            }
            throw new DuplicateHotelNameException("Given hotel name already exists");
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
            var hotelsList = _hotelReadRepository.GetAll();
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

        public async Task<PaginatedResult<HotelGetDto>> GetHotelsPaginated(int page, int pageSize)
        {
            var query = _hotelReadRepository.GetAllByExpressionOrderBy
                (hotel => hotel.IsDeleted == false,
                pageSize,
                (page - 1) * pageSize,
                hotel => hotel.DateCreated);
            var hotels = await query.ToListAsync();
            var totalCount = await _hotelReadRepository.GetAll().CountAsync();
            var paginatedResult = new PaginatedResult<HotelGetDto>
            {
                Items = _mapper.Map<List<HotelGetDto>>(hotels),
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
            return paginatedResult;
        }
    }
}
