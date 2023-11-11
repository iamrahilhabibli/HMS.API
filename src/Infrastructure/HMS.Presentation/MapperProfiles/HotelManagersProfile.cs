using AutoMapper;
using HMS.Application.DTOs.Auth_DTOs;
using HMS.Domain.Entities;

namespace HMS.Persistence.MapperProfiles
{
    public class HotelManagersProfile:Profile
    {
        public HotelManagersProfile()
        {
            CreateMap<HotelManager,UserRegisterDto>().ReverseMap();
        }
    }
}
