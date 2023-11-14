using HMS.Application.Abstraction.Repositories.IHotelRepositories;
using HMS.Domain.Entities;
using HMS.Persistence.Context;

namespace HMS.Persistence.Implementations.Repositories.HotelRepositories
{
    public class HotelReadRepository : ReadRepository<Hotel>, IHotelReadRepository
    {
        public HotelReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
