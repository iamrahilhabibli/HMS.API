using AutoMapper;
using HMS.Application.Abstraction.Repositories.IHotelManagerRepositories;
using HMS.Application.Abstraction.Repositories.IVisitorRepositories;
using HMS.Application.Abstraction.Services;
using HMS.Application.DTOs.Auth_DTOs;
using HMS.Domain.Entities;
using HMS.Domain.Identity;
using HMS.Persistence.Exceptions;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace HMS.Persistence.Implementations.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHotelManagerWriteRepository _hotelManagerWriteRepository;
        private readonly IVisitorWriteRepository _visitorWriteRepository;

        public AuthService(IMapper mapper,
                           UserManager<AppUser> userManager,
                           IHotelManagerWriteRepository hotelManagerWriteRepository,
                           IVisitorWriteRepository visitorWriteRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _hotelManagerWriteRepository = hotelManagerWriteRepository;
            _visitorWriteRepository = visitorWriteRepository;
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
        public async Task Register(UserRegisterDto userRegisterDto)
        {
            if (userRegisterDto is null) { throw new ArgumentNullException(); }
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
            if (userRegisterDto.role == Domain.Enums.Roles.Admin)
            {
                HotelManager newManager = _mapper.Map<HotelManager>(userRegisterDto);
                newManager.AppUser = newUser;
                await _hotelManagerWriteRepository.AddAsync(newManager);
                await _hotelManagerWriteRepository.SaveChangeAsync();

            }
            if (userRegisterDto.role == Domain.Enums.Roles.Visitor)
            {
                Visitor newVisitor = _mapper.Map<Visitor>(userRegisterDto);
                await _visitorWriteRepository.AddAsync(newVisitor);
                await _visitorWriteRepository.SaveChangeAsync();
            }
        }
    }
}
