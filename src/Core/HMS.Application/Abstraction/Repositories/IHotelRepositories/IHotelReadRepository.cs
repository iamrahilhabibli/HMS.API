using HMS.Application.DTOs.Hotel_DTOs;
using HMS.Domain.Entities;

namespace HMS.Application.Abstraction.Repositories.IHotelRepositories
{
    public interface IHotelReadRepository:IReadRepository<Hotel>
    {
        Task<List<Hotel>> GetAllAsync();
    }
}
