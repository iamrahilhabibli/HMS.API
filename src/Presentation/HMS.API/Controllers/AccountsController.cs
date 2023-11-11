using HMS.Application.Abstraction.Services;
using HMS.Application.DTOs.Auth_DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountsController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterAccount(UserRegisterDto userRegisterDto)
        {
            await _authService.Register(userRegisterDto);
            return StatusCode((int)HttpStatusCode.Created);
        }
    }
}
