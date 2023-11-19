using AutoMapper;
using HMS.Application.Abstraction.Repositories.IAmenityRepositories;
using HMS.Application.Abstraction.Services;
using HMS.Application.DTOs.Amenity_DTOs;
using HMS.Domain.Entities;

namespace HMS.Persistence.Implementations.Services
{
    public class AmenityService : IAmenityService
    {
        private readonly IMapper _mapper;
        private readonly IAmenityWriteRepository _amenityWriteRepository;   

        public AmenityService(IMapper mapper, IAmenityWriteRepository amenityWriteRepository)
        {
            _mapper = mapper;
            _amenityWriteRepository = amenityWriteRepository;
        }

        public async Task CreateAmenity(AmenityCreateDto amenityCreateDto)
        {
            if (amenityCreateDto is null) throw new ArgumentNullException();
            var newAmenity = _mapper.Map<Amenity>(amenityCreateDto);
            await _amenityWriteRepository.AddAsync(newAmenity);
            await _amenityWriteRepository.SaveChangeAsync();
        }
    }
}
