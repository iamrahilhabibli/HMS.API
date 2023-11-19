using HMS.Application.Abstraction.Repositories.IHotelManagerRepositories;
using HMS.Domain.Entities;
using HMS.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HMS.Persistence.Implementations.Repositories.HotelManagerRepositories
{
    public class HotelManagerReadRepository : ReadRepository<HotelManager>, IHotelManagerReadRepository
    {
        public HotelManagerReadRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<HotelManager?> GetHotelManagerByAppUserId(Expression<Func<HotelManager, bool>> expression)
        {
            HotelManager? hotelManager = await Table.SingleOrDefaultAsync(expression);
            return hotelManager;
        }
    }
}
