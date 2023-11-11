using AutoMapper;
using HMS.Application.Abstraction.Repositories.IHotelManagerRepositories;
using HMS.Application.Abstraction.Repositories.IVisitorRepositories;
using HMS.Application.Abstraction.Services;
using HMS.Application.DTOs.Auth_DTOs;
using HMS.Application.DTOs.Response_DTOs;
using HMS.Domain.Entities;
using HMS.Domain.Identity;
using HMS.Persistence.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Text;

namespace HMS.Persistence.Implementations.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHotelManagerWriteRepository _hotelManagerWriteRepository;
        private readonly IVisitorWriteRepository _visitorWriteRepository;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtService _jwtService;

        public AuthService(IMapper mapper,
                           UserManager<AppUser> userManager,
                           IHotelManagerWriteRepository hotelManagerWriteRepository,
                           IVisitorWriteRepository visitorWriteRepository,
                           SignInManager<AppUser> signInManager,
                           IJwtService jwtService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _hotelManagerWriteRepository = hotelManagerWriteRepository;
            _visitorWriteRepository = visitorWriteRepository;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }
        private void HandleIdentityErrors(IdentityResult identityResult)
        {
            StringBuilder errorMessage = new();
            foreach (var error in identityResult.Errors)
            {
                errorMessage.AppendLine(error.Description);
            }
            throw new AccountsException(errorMessage.ToString());
        }
        private async Task HandleHotelManager(AppUser newUser ,UserRegisterDto userRegisterDto)
        {
            HotelManager newManager = _mapper.Map<HotelManager>(userRegisterDto);
            newManager.AppUser = newUser;
            await _hotelManagerWriteRepository.AddAsync(newManager);
            await _hotelManagerWriteRepository.SaveChangeAsync();
        }
        private async Task HandleVisitor(AppUser newUser, UserRegisterDto userRegisterDto)
        {
            Visitor newVisitor = _mapper.Map<Visitor>(userRegisterDto);
            newVisitor.AppUser = newUser;
            await _visitorWriteRepository.AddAsync(newVisitor);
            await _visitorWriteRepository.SaveChangeAsync();
        }
        private async Task<bool> IsEmailAvailable(string email)
        {
            var result = await _userManager.FindByEmailAsync(email);
            return result != null;
        }
        private async Task<TokenResponseDto> TokenGenerator(AppUser user)
        {
            TokenResponseDto tokenResponseDto = await _jwtService.CreateJwt(user);
            user.RefreshToken = tokenResponseDto?.refreshToken;
            user.RefreshTokenExpiration = tokenResponseDto?.refreshTokenExpiration;
            await _userManager.UpdateAsync(user);
            return tokenResponseDto;
        }
        public async Task Register(UserRegisterDto userRegisterDto)
        {
            if (userRegisterDto is null) { throw new ArgumentNullException(); }
            await IsEmailAvailable(userRegisterDto.email);
            AppUser newUser = _mapper.Map<AppUser>(userRegisterDto);
            IdentityResult identityResult = await _userManager.CreateAsync(newUser, userRegisterDto.password);
            if (!identityResult.Succeeded)
            {
                HandleIdentityErrors(identityResult);
            }
            var result = await _userManager.AddToRoleAsync(newUser, userRegisterDto.role.ToString());
            if (!result.Succeeded)
            {
                HandleIdentityErrors(result);
            }
            switch (userRegisterDto.role)
            {
                case Domain.Enums.Roles.Admin:
                    await HandleHotelManager(newUser, userRegisterDto);
                    break;
                case Domain.Enums.Roles.Visitor:
                    await HandleVisitor(newUser, userRegisterDto);
                    break;
                default:
                    break;
            }
        }
        public async Task<TokenResponseDto> Login(UserSignInDto userSignInDto)
        {
            if (userSignInDto is null) { throw new ArgumentNullException(); }
            AppUser user = await _userManager.FindByEmailAsync(userSignInDto.email) ?? throw new ArgumentException();
            SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(user, userSignInDto.password, true);
            if (!signInResult.Succeeded) { throw new Exception(); }
            return await TokenGenerator(user);
        }
    }
}
