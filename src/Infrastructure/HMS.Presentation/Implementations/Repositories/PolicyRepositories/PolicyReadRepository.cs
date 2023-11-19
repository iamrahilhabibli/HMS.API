using HMS.Application.Abstraction.Repositories.IPolicyRepositories;
using HMS.Domain.Entities;
using HMS.Persistence.Context;

namespace HMS.Persistence.Implementations.Repositories.PolicyRepositories
{
    public class PolicyReadRepository : ReadRepository<HotelPolicy>, IPolicyReadRepository
    {
        public PolicyReadRepository(AppDbContext context) : base(context) { }
    }
}
