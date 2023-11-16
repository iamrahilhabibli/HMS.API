using HMS.Application.Abstraction.Repositories.IHotelRepositories;
using HMS.Application.DTOs.Hotel_DTOs;
using HMS.Domain.Entities;
using HMS.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HMS.Persistence.Implementations.Repositories.HotelRepositories
{
    public class HotelReadRepository : ReadRepository<Hotel>, IHotelReadRepository
    {
        public HotelReadRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Hotel>> GetAllAsync()
        {
            var hotels = await Table.ToListAsync();
            return hotels;
        }
    }
}
