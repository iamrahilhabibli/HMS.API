using HMS.Application.DTOs.Auth_DTOs;

namespace HMS.Application.Abstraction.Services
{
    public interface IAuthService
    {
        Task Register(UserRegisterDto userRegisterDto);
    }
}
