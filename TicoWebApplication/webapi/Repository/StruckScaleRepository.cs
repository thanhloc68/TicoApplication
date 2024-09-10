using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Helpers.QueryStruckScale;
using webapi.Interface;
using webapi.Models;
namespace webapi.Repository
{
    public class StruckScaleRepository : IStruckScaleRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public StruckScaleRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<StruckScales?> CreateStruckScale(StruckScales struckInfo)
        {
            await _dbContext.AddAsync(struckInfo);
            await _dbContext.SaveChangesAsync();
            return struckInfo;
        }

        public async Task<StruckScales?> DeleteStruckScale(int id)
        {
            var query = await _dbContext.StruckScale.FirstOrDefaultAsync(x => x.id == id);
            if (query == null) return null;
            _dbContext.StruckScale.Remove(query);
            await _dbContext.SaveChangesAsync();
            return query;
        }

        public async Task<List<StruckScales>?> GetAllStruckScaleAsync(QueryObjectStruckScale query)
        {
            var list = _dbContext.StruckScale.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.styleScale)) list = list.Where(x => x.styleScale != null && x.styleScale.Contains(query.styleScale));
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                list = query.SortBy.ToLower() switch
                {
                    "id" => query.isDecsending ? list.OrderByDescending(s => s.id) : list.OrderBy(s => s.id),
                    _ => list // Nếu SortBy không khớp với bất kỳ giá trị nào, không thay đổi thứ tự
                };
            }
            var data = await list.ToListAsync();
            return data;
        }

        public async Task<StruckScales?> GetAllStruckScaleByIdAsync(int id)
        {
            var query = await _dbContext.StruckScale.FindAsync(id);
            if (query == null) return null;
            return query;
        }

        public async Task<StruckScales?> UpdateStruckScale(int id, StruckScales struckInfo)
        {
            var query = await _dbContext.StruckScale.FirstOrDefaultAsync(x => x.id == id);
            if (query == null) return null;
            query.firstScale = struckInfo.firstScale;
            query.secondScale = struckInfo.secondScale;
            query.results = struckInfo.results;
            query.styleScale = struckInfo.styleScale;
            query.firstScaleDate = struckInfo.firstScaleDate;
            query.secondScaleDate = struckInfo.secondScaleDate;
            query.createDate = struckInfo.createDate;
            query.isDone = struckInfo.isDone;
            query.struckID = struckInfo.struckID;
            await _dbContext.SaveChangesAsync();
            return query;
        }
    }
}
