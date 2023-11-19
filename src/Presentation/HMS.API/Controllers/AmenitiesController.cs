using System.Net;
using HMS.Application.Abstraction.Services;
using HMS.Application.DTOs.Amenity_DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Master")]
    public class AmenitiesController : ControllerBase
    {
        private readonly IAmenityService _amenityService;

        public AmenitiesController(IAmenityService amenityService)
        {
            _amenityService = amenityService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateAmenity(AmenityCreateDto amenityCreateDto)
        {
            await _amenityService.CreateAmenity(amenityCreateDto);
            return (StatusCode((int)HttpStatusCode.Created));
        }
    }
}
