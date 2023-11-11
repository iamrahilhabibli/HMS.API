using HMS.Application.Abstraction.Repositories.IVisitorRepositories;
using HMS.Domain.Entities;
using HMS.Persistence.Context;

namespace HMS.Persistence.Implementations.Repositories.VisitorRepositories
{
    public class VisitorReadRepository : ReadRepository<Visitor>, IVisitorReadRepository
    {
        public VisitorReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
