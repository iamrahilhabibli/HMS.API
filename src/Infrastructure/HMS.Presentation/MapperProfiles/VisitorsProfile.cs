using AutoMapper;
using HMS.Application.DTOs.Auth_DTOs;
using HMS.Domain.Entities;

namespace HMS.Persistence.MapperProfiles
{
    public class VisitorsProfile : Profile
    {
        public VisitorsProfile()
        {
            CreateMap<Visitor, UserRegisterDto>().ReverseMap();
        }
    }
}
