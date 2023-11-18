using HMS.Application.Abstraction.Services;
using HMS.Application.DTOs.Hotel_DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        [HttpGet("[action]")]
        public IActionResult GetAllHotels()
        {
            var response = _hotelService.GetAllHotels();
            return Ok(response);
        }
        [HttpGet("Details")]
        public async Task<IActionResult> GetDetails([FromQuery]Guid Id)
        {
            HotelGetDto hotelGetDto = await _hotelService.GetHotelById(Id);
            return Ok(hotelGetDto);
        }
            [HttpGet("HotelsList")]
            public async Task<IActionResult> ListedHotels([FromQuery] int pageSize = 3, [FromQuery] int page = 1)
            {
                var response = await _hotelService.GetHotelsPaginated(page, pageSize);
                return Ok(response);
            }
    }
}
