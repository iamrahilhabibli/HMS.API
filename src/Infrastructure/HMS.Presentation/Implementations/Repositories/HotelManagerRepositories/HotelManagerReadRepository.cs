using HMS.Application.Abstraction.Repositories.IHotelManagerRepositories;
using HMS.Domain.Entities;
using HMS.Persistence.Context;

namespace HMS.Persistence.Implementations.Repositories.HotelManagerRepositories
{
    public class HotelManagerReadRepository : ReadRepository<HotelManager>, IHotelManagerReadRepository
    {
        public HotelManagerReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
