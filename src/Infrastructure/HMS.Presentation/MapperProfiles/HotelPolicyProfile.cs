using AutoMapper;
using HMS.Application.DTOs.Hotel_DTOs;
using HMS.Domain.Entities;

namespace HMS.Persistence.MapperProfiles
{
    public class HotelPolicyProfile:Profile
    {
        public HotelPolicyProfile()
        {
            CreateMap<HotelPolicy, HotelCreateDto>().ReverseMap();
        }
    }
}
