using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.DTOs.TankPump;
using webapi.Helpers.QueryTankPump;
using webapi.Interface;
using webapi.Models;

namespace webapi.Repository
{
    public class TankStruckRepository : ITankStruckRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public TankStruckRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<TankStrucks?> CreateTankPump(TankStrucks tankStrucks)
        {
            await _dbContext.AddAsync(tankStrucks);
            await _dbContext.SaveChangesAsync();
            return tankStrucks;
        }

        public async Task<TankStrucks?> DeleteTankPump(int id)
        {
            var query = await _dbContext.TankStruck.FirstOrDefaultAsync(x => x.id == id);
            if (query == null) return null;
            _dbContext.TankStruck.Remove(query);
            await _dbContext.SaveChangesAsync();
            return query;
        }

        public async Task<List<TankStrucks>> GetAllTankPumpAsync(QueryObjectTankPump query)
        {
            var list = _dbContext.TankStruck
                .Include(x => x.StruckInfo)
                .AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.sourceOfGoods)) list = list.Where(x => x.sourceOfGoods != null && x.sourceOfGoods.Contains(query.sourceOfGoods));
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                list = query.SortBy.ToLower() switch
                {
                    "id" => query.isDecsending ? list.OrderByDescending(x => x.id) : list.OrderBy(x => x.id),
                    _ => list // Nếu SortBy không khớp với bất kỳ giá trị nào, không thay đổi thứ tự
                };
            }
            var data = await list.ToListAsync();
            return data;
        }

        public async Task<TankStrucks?> GetAllTankPumpByIdAsync(int id)
        {
            var query = await _dbContext.TankStruck.FindAsync(id);
            if (query == null) return null;
            return query;
        }

        public async Task<TankStrucks?> UpdateTankPump(int id, UpdateTankPumpDTO? tankPumps)
        {
            var query = await _dbContext.TankStruck.FirstOrDefaultAsync(x => x.id == id);
            if (query == null) return null;

            // Update sourceOfGoods and requestedVolume if not set and processing hasn't started
            if (query.requestedVolume != 0 && query.processing != 0 && string.IsNullOrEmpty(query.sourceOfGoods))
            {
                query.sourceOfGoods = tankPumps?.sourceOfGoods ?? query.sourceOfGoods;
                query.requestedVolume = tankPumps?.requestedVolume ?? query.requestedVolume;

                // Start processing if all necessary fields are filled
                if (query.requestedVolume != 0 && !string.IsNullOrEmpty(query.sourceOfGoods))
                {
                    query.processing = 1; // Mark as processing
                    query.startTimePump = DateTime.Now;
                }
            }

            // Handle pumping progress
            if (query.requestedVolume > 0 && query.processing == 1 && tankPumps?.pumpVolume <= query.requestedVolume)
            {
                query.pumpVolume = tankPumps.pumpVolume ?? query.pumpVolume; // Only update if pumpVolume is provided
                query.endTimePump = DateTime.Now;

                // Complete processing if pumpVolume matches requestedVolume
                if (query.pumpVolume == query.requestedVolume && query.processing != 2)
                {
                    query.processing = 2; // Mark as completed
                }
            }

            // Save all changes after the updates
            await _dbContext.SaveChangesAsync();

            return query;
        }
    }
}