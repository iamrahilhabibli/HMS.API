using HMS.Application.Abstraction.Services;
using HMS.Application.DTOs.Hotel_DTOs;
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
        [HttpPost("[action]/{appUserId}")]
        public async Task<IActionResult> CreateHotel(string appUserId, [FromBody] HotelCreateDto hotelCreateDto)
        {
            await _hotelService.CreateHotel(appUserId, hotelCreateDto);
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpGet("[action]")]
        public IActionResult GetAllHotels()
        {
            var response = _hotelService.GetAllHotels();
            return Ok(response);
        }
        [HttpGet("details")]
        public async Task<IActionResult> GetDetails([FromQuery] Guid Id)
        {
            HotelGetDto hotelGetDto = await _hotelService.GetHotelById(Id);
            return Ok(hotelGetDto);
        }
        [HttpGet("hotelslist")]
        public async Task<IActionResult> ListedHotels([FromQuery] int pageSize = 3, [FromQuery] int page = 1)
        {
            var response = await _hotelService.GetHotelsPaginated(page, pageSize);
            return Ok(response);
        }
        [HttpDelete("deletehotel/{Id}")]
        public async Task<IActionResult> DeleteHotel(Guid Id)
        {
            await _hotelService.DeleteHotel(Id);
            return StatusCode((int)HttpStatusCode.NoContent);
        }

        [HttpGet("[action]/{appUserId}")]
        public async Task<IActionResult> CheckHotelExists(string appUserId)
        {
            var response = await _hotelService.CheckManagerRegisteredHotel(appUserId);
            return Ok(response);
        }
    }
}
