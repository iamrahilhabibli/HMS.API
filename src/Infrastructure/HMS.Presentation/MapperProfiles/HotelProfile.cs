using AutoMapper;
using HMS.Application.DTOs.Hotel_DTOs;
using HMS.Domain.Entities;

namespace HMS.Persistence.MapperProfiles
{
    public class HotelProfile : Profile
    {
        public HotelProfile()
        {
            CreateMap<Hotel, HotelCreateDto>().ReverseMap();
            CreateMap<Hotel, HotelGetDto>().ReverseMap();
        }
    }
}
