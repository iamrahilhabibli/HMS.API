using HMS.Application.Abstraction.Services;
using HMS.Application.DTOs.Auth_DTOs;
using HMS.Application.DTOs.Response_DTOs;
using HMS.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountsController(IAuthService authService, SignInManager<AppUser> signInManager)
        {
            _authService = authService;
            _signInManager = signInManager;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterAccount(UserRegisterDto userRegisterDto)
        {
            if (userRegisterDto.password != userRegisterDto.passwordConfirm) 
            { return StatusCode((int)HttpStatusCode.BadRequest); }
            await _authService.Register(userRegisterDto);
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(UserSignInDto userSignInDto)
        {
            TokenResponseDto response = await _authService.Login(userSignInDto);
            return Ok(response);
        }
        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> ValidateRefreshToken(string refreshToken)
        {
            TokenResponseDto response = await _authService.ValidateRefreshToken(refreshToken);
            return Ok(response);
        }
        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
