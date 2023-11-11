using HMS.Application.Abstraction.Repositories.IVisitorRepositories;
using HMS.Domain.Entities;
using HMS.Persistence.Context;

namespace HMS.Persistence.Implementations.Repositories.VisitorRepositories
{
    public class VisitorWriteRepository : WriteRepository<Visitor>, IVisitorWriteRepository
    {
        public VisitorWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
