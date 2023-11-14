using HMS.Application.Abstraction.Repositories.IHotelRepositories;
using HMS.Domain.Entities;
using HMS.Persistence.Context;

namespace HMS.Persistence.Implementations.Repositories.HotelRepositories
{
    public class HotelWriteRepository : WriteRepository<Hotel>, IHotelWriteRepository
    {
        public HotelWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
