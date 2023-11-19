using HMS.Application.Abstraction.Repositories.IAmenityRepositories;
using HMS.Domain.Entities;
using HMS.Persistence.Context;

namespace HMS.Persistence.Implementations.Repositories.AmenityRepositories
{
    public class AmenityWriteRepository : WriteRepository<Amenity>, IAmenityWriteRepository

    {
        public AmenityWriteRepository(AppDbContext context) : base(context) { }
    }
}
