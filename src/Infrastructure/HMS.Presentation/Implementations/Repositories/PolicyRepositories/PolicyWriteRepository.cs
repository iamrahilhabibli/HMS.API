using HMS.Application.Abstraction.Repositories.IPolicyRepositories;
using HMS.Domain.Entities;
using HMS.Persistence.Context;

namespace HMS.Persistence.Implementations.Repositories.PolicyRepositories
{
    public class PolicyWriteRepository : WriteRepository<HotelPolicy>, IPolicyWriteRepository
    {
        public PolicyWriteRepository(AppDbContext context) : base(context) { }
    }
}
