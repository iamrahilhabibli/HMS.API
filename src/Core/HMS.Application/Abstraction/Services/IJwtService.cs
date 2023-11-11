using HMS.Application.DTOs.Response_DTOs;
using HMS.Domain.Identity;

namespace HMS.Application.Abstraction.Services
{
    public interface IJwtService
    {
        Task<TokenResponseDto> CreateJwt(AppUser user);
    }
}
