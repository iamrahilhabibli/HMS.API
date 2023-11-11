using HMS.Application.Abstraction.Repositories.IHotelManagerRepositories;
using HMS.Domain.Entities;
using HMS.Persistence.Context;

namespace HMS.Persistence.Implementations.Repositories.HotelManagerRepositories
{
    public class HotelManagerWriteRepository : WriteRepository<HotelManager>, IHotelManagerWriteRepository
    {
        public HotelManagerWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
