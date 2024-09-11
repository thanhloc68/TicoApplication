using webapi.DTOs.TankPump;
using webapi.Helpers.QueryTankPump;
using webapi.Models;

namespace webapi.Interface
{
    public interface ITankStruckRepository
    {
        Task<List<TankStrucks>> GetAllStruckScaleAsync(QueryObjectTankPump query);
        Task<TankStrucks?> GetAllStruckScaleByIdAsync(int id);
        Task<TankStrucks?> CreateStruckScale(TankStrucks struckInfo);
        Task<TankStrucks?> UpdateStruckScale(int id, UpdateTankPumpDTO struckInfo);
        Task<TankStrucks?> DeleteStruckScale(int id);
    }
}
