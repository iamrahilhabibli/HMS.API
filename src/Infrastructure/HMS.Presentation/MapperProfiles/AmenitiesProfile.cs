using AutoMapper;
using HMS.Application.DTOs.Amenity_DTOs;
using HMS.Domain.Entities;

namespace HMS.Persistence.MapperProfiles
{
    public class AmenitiesProfile : Profile
    {
        public AmenitiesProfile()
        {
            CreateMap<Amenity, AmenityCreateDto>().ReverseMap();
        }
    }
}
