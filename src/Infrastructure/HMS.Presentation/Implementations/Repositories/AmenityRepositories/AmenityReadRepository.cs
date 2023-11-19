using HMS.Application.Abstraction.Repositories.IAmenityRepositories;
using HMS.Domain.Entities;
using HMS.Persistence.Context;

namespace HMS.Persistence.Implementations.Repositories.AmenityRepositories
{
    public class AmenityReadRepository : ReadRepository<Amenity>, IAmenityReadRepository
    {
        public AmenityReadRepository(AppDbContext context) : base(context) { }
    }
}
