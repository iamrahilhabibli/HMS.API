using HMS.Application.DTOs.Auth_DTOs;
using HMS.Application.DTOs.Response_DTOs;

namespace HMS.Application.Abstraction.Services
{
    public interface IAuthService
    {
        Task Register(UserRegisterDto userRegisterDto);
        Task<TokenResponseDto> Login(UserSignInDto userSignInDto);
    }
}
