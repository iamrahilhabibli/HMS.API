using AutoMapper;
using HMS.Application.DTOs.Auth_DTOs;
using HMS.Domain.Identity;

namespace HMS.Persistence.MapperProfiles
{
    public class AccountsProfile:Profile
    {
        public AccountsProfile()
        {
            CreateMap<AppUser, UserRegisterDto>().ReverseMap();
        }
    }
}
