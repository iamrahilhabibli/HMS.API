using AutoMapper;
using HMS.Application.Abstraction.Repositories.IHotelManagerRepositories;
using HMS.Application.Abstraction.Repositories.IHotelRepositories;
using HMS.Application.Abstraction.Repositories.IPolicyRepositories;
using HMS.Application.Abstraction.Services;
using HMS.Application.DTOs.Hotel_DTOs;
using HMS.Application.Wrappers;
using HMS.Domain.Entities;
using HMS.Persistence.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HMS.Persistence.Implementations.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelWriteRepository _hotelWriteRepository;
        private readonly IHotelReadRepository _hotelReadRepository;
        private readonly IPolicyWriteRepository _policyWriteRepository;
        private readonly IHotelManagerReadRepository _hotelManagerReadRepository;
        private readonly IHotelManagerWriteRepository _hotelManagerWriteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public HotelService(IHotelWriteRepository hotelWriteRepository,
                            IMapper mapper,
                            IHotelReadRepository hotelReadRepository, IPolicyWriteRepository policyWriteRepository, IHotelManagerReadRepository hotelManagerReadRepository, IHotelManagerWriteRepository hotelManagerWriteRepository)
        {
            _hotelWriteRepository = hotelWriteRepository;
            _mapper = mapper;
            _hotelReadRepository = hotelReadRepository;
            _policyWriteRepository = policyWriteRepository;
            _hotelManagerReadRepository = hotelManagerReadRepository;
            _hotelManagerWriteRepository = hotelManagerWriteRepository;
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
        private async Task SetHotelManager(string appUserId, Hotel newHotel)
        {
            var hotelManager = await _hotelManagerReadRepository.GetHotelManagerByAppUserId(manager => manager.AppUserId == appUserId);
            if(hotelManager != null) 
            {
                hotelManager.Hotel = newHotel; 
                await _hotelManagerWriteRepository.SaveChangeAsync(); 
                return; 
            }
            throw new InvalidOperationException($"HotelManager with {appUserId} does not exist, something went wrong");
        }
        private async Task<bool> HotelManagerHotelExists(string appUserId)
        {
            var hotelManager = await _hotelManagerReadRepository.GetByExpressionAsync(hotelManager => hotelManager.AppUserId == appUserId && hotelManager.HotelId != null);
            return hotelManager != null;
        }
        public async Task CreateHotel(string appUserId, HotelCreateDto hotelCreateDto)
        {
            if (hotelCreateDto is null) { throw new ArgumentNullException(); }
            var hotelManagerHotelExists = await HotelManagerHotelExists(appUserId);
            if (hotelManagerHotelExists) throw new ManagerHotelRegistered("Given User already has a registered hotel");
            var hotelExists = await HotelExists(hotelCreateDto);
            if (hotelExists) throw new DuplicateHotelNameException("Given hotel name already exists");
            var newHotel = _mapper.Map<Hotel>(hotelCreateDto);
            newHotel.Policies = await CreateHotelPolicies(hotelCreateDto, newHotel);
            await SetHotelManager(appUserId, newHotel);
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
            var hotel = await _hotelReadRepository.GetByExpressionAsync(hotel => hotel.Id ==  id, true, "Policies", "Manager");
            if(hotel != null) 
            {
                hotel.IsDeleted = true;
                hotel.Policies.IsDeleted = true;
                hotel.Manager.Hotel = null;
                await _hotelWriteRepository.SaveChangeAsync(); 
                return; 
            }
            throw new NotFoundException($"Hotel with {id} has not been found");
        }

        public async Task<bool> CheckManagerRegisteredHotel(string appUserId)
        {
            if (string.IsNullOrEmpty(appUserId)) {
                _logger.LogError("Given parameter is null or empty");
                throw new ArgumentNullException($"{appUserId} Given User ID is not valid"); }
            var result = await _hotelManagerReadRepository.GetByExpressionAsync(hotelManager => hotelManager.AppUserId == appUserId && hotelManager.Hotel == null);
            if (result != null) { return true; }
            return false;
        }
    }
}
