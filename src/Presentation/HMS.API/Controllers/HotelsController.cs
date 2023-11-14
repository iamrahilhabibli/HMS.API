using HMS.Application.Abstraction.Services;
using HMS.Application.DTOs.Hotel_DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateHotel([FromBody] HotelCreateDto hotelCreateDto)
        {
            await _hotelService.CreateHotel(hotelCreateDto);
            return StatusCode((int)HttpStatusCode.Created);
        }
    }
}
