namespace HMS.Application.DTOs.Response_DTOs
{
    public record TokenResponseDto(string jwt, DateTime jwtExpiration, string refreshToken, DateTime refreshTokenExpiration);
}
