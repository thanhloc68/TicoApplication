using webapi.DTOs.TankPump;
using webapi.Helpers.QueryTankPump;
using webapi.Models;

namespace webapi.Interface
{
    public interface ITankStruckRepository
    {
        Task<List<TankStrucks>> GetAllTankPumpAsync(QueryObjectTankPump query);
        Task<TankStrucks?> GetAllTankPumpByIdAsync(int id);
        Task<TankStrucks?> CreateTankPump(TankStrucks tankpumps);
        Task<TankStrucks?> UpdateTankPump(int id, UpdateTankPumpDTO tankpumps);
        Task<TankStrucks?> DeleteTankPump(int id);
    }
}
