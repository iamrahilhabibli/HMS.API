using HMS.Application.DTOs.Amenity_DTOs;

namespace HMS.Application.Abstraction.Services
{
    public interface IAmenityService
    {
        Task CreateAmenity(AmenityCreateDto amenityCreateDto);
    }
}
