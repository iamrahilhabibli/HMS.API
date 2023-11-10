using HMS.Domain.Enums;

namespace HMS.Application.DTOs.Auth_DTOs
{
    public record UserRegisterDto(string userName, string firstName, string lastName, string email, string password, string passwordConfirm, string phoneNumber, Roles role);
}
