using AutoMapper;
using HMS.Application.Abstraction.Repositories.IHotelRepositories;
using HMS.Application.Abstraction.Repositories.IPolicyRepositories;
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
        private readonly IPolicyWriteRepository _policyWriteRepository;
        private readonly IMapper _mapper;

        public HotelService(IHotelWriteRepository hotelWriteRepository,
                            IMapper mapper,
                            IHotelReadRepository hotelReadRepository, IPolicyWriteRepository policyWriteRepository)
        {
            _hotelWriteRepository = hotelWriteRepository;
            _mapper = mapper;
            _hotelReadRepository = hotelReadRepository;
            _policyWriteRepository = policyWriteRepository;
        }

        private async Task<bool> HotelExists(HotelCreateDto hotelCreateDto)
        {
            var hotel = await _hotelReadRepository.GetByExpressionAsync(hotel => hotel.Name == hotelCreateDto.Name);
            return hotel != null;
        }

        private async Task<HotelPolicy> CreateHotelPolicies(HotelCreateDto hotelCreateDto, Hotel newHotel)
        {
            var newHotelPolicies = _mapper.Map<HotelPolicy>(hotelCreateDto);
            newHotelPolicies.Hotel = newHotel;
            await _policyWriteRepository.AddAsync(newHotelPolicies);
            await _policyWriteRepository.SaveChangeAsync();
            return newHotelPolicies;
        }
        public async Task CreateHotel(HotelCreateDto hotelCreateDto)
        {
            if (hotelCreateDto is null) { throw new ArgumentNullException(); }
            var hotelExists = await HotelExists(hotelCreateDto);
            if (hotelExists) throw new DuplicateHotelNameException("Given hotel name already exists");
            var newHotel = _mapper.Map<Hotel>(hotelCreateDto);
            newHotel.Policies = await CreateHotelPolicies(hotelCreateDto, newHotel);
        }

        public async Task<HotelGetDto> GetHotelById(Guid id)
        {
            if (id == Guid.Empty) { throw new ArgumentNullException(); }
            var hotel = await _hotelReadRepository.GetByIdAsync(id);
            if (hotel is not Hotel) { throw new Exception(); }
            var hotelGetDto = _mapper.Map<HotelGetDto>(hotel);
            return hotelGetDto;
        }

        public List<HotelGetDto> GetAllHotels()
        {
            var hotelsList = _hotelReadRepository.GetAllByExpression(hotel => hotel.IsDeleted == false, int.MaxValue, 0);
            var hotelGetDtos = _mapper.Map<List<HotelGetDto>>(hotelsList);
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

        public async Task UpdateHotelById(Guid id, HotelUpdateDto hotelUpdateDto)
        {
            if (id == Guid.Empty) throw new ArgumentNullException();
            var hotel = await _hotelReadRepository.GetByIdAsync(id);
        }

        public async Task DeleteHotel(Guid id)
        {
            if(id == Guid.Empty) { throw new ArgumentNullException(); }
            var hotel = await _hotelReadRepository.GetByExpressionAsync(hotel => hotel.Id ==  id, true, "Policies");
            if(hotel != null) 
            {
                hotel.IsDeleted = true;
                hotel.Policies.IsDeleted = true;
                await _hotelWriteRepository.SaveChangeAsync(); 
                return; 
            }
            throw new NotFoundException($"Hotel with {id} has not been found");
        }
    }
}
