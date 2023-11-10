using AutoMapper;
using HMS.Application.Abstraction.Services;
using HMS.Application.DTOs.Auth_DTOs;
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

        public AuthService(IMapper mapper,
                           UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task Register(UserRegisterDto userRegisterDto)
        {
            if (userRegisterDto is null) { throw new ArgumentNullException(); }
            AppUser newUser = _mapper.Map<AppUser>(userRegisterDto);
            IdentityResult identityResult = await _userManager.CreateAsync(newUser, userRegisterDto.password);
            if (!identityResult.Succeeded) 
            { 
                StringBuilder errorMessage = new();
                foreach (var error in identityResult.Errors)
                {
                    errorMessage.AppendLine(error.Description);
                }
                throw new AccountsException(errorMessage.ToString());
            }
            var result = await _userManager.AddToRoleAsync(newUser, userRegisterDto.role.ToString());
            if (!result.Succeeded) 
            { 
                StringBuilder errorMessage = new();
                foreach (var error in result.Errors)
                {
                    errorMessage.AppendLine(error.Description);
                }
                throw new AccountsException(errorMessage.ToString());
            }

        }
    }
}
