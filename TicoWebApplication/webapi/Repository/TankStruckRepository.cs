using Microsoft.EntityFrameworkCore;
using webapi.Data;
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
        public async Task<TankStrucks?> CreateStruckScale(TankStrucks tankStrucks)
        {
            await _dbContext.AddAsync(tankStrucks);
            await _dbContext.SaveChangesAsync();
            return tankStrucks;
        }

        public async Task<TankStrucks?> DeleteStruckScale(int id)
        {
            var query = await _dbContext.TankStruck.FirstOrDefaultAsync(x => x.id == id);
            if (query == null) return null;
            _dbContext.TankStruck.Remove(query);
            await _dbContext.SaveChangesAsync();
            return query;
        }

        public async Task<List<TankStrucks>?> GetAllStruckScaleAsync(QueryObjectTankPump query)
        {
            var list = _dbContext.TankStruck.AsNoTracking().AsQueryable();
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

        public async Task<TankStrucks?> GetAllStruckScaleByIdAsync(int id)
        {
            var query = await _dbContext.TankStruck.FindAsync(id);
            if (query == null) return null;
            return query;
        }

        public async Task<TankStrucks?> UpdateStruckScale(int id, TankStrucks tankStrucks)
        {
            var query = await _dbContext.TankStruck.FirstOrDefaultAsync(x => x.id == id);
            if (query == null) return null;
            query.pumpVolume = tankStrucks?.pumpVolume;
            query.requestedVolume = tankStrucks?.requestedVolume;
            query.sourceOfGoods = tankStrucks?.sourceOfGoods;
            query.startTimePump = tankStrucks?.startTimePump;
            query.endTimePump = tankStrucks?.endTimePump;
            query.createDate = tankStrucks?.createDate;
            query.processing = tankStrucks?.processing;
            await _dbContext.SaveChangesAsync();
            return query;
        }
    }
}
