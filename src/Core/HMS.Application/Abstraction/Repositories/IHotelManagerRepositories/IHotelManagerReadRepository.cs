using HMS.Domain.Entities;
using System.Linq.Expressions;

namespace HMS.Application.Abstraction.Repositories.IHotelManagerRepositories
{
    public interface IHotelManagerReadRepository : IReadRepository<HotelManager> 
    {
        Task<HotelManager?> GetHotelManagerByAppUserId(Expression<Func<HotelManager, bool>> expression);
    }
}
