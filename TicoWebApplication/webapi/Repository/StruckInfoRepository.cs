using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Helpers.QueryStruckInfomation;
using webapi.Interface;
using webapi.Models;

namespace webapi.Repository
{
    public class StruckInfoRepository : IStuckInfomationRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public StruckInfoRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StruckInfo?> CreateStruckInfo(StruckInfo struckInfo)
        {
            await _dbContext.AddAsync(struckInfo);
            await _dbContext.SaveChangesAsync();
            return struckInfo;
        }

        public async Task<StruckInfo?> DeleteStruckInfo(int id)
        {
            var query = await _dbContext.StruckInfo.FirstOrDefaultAsync(x => x.id == id);
            if (query == null) return null;
            _dbContext.StruckInfo.Remove(query);
            await _dbContext.SaveChangesAsync();
            return query;
        }

        public async Task<List<StruckInfo>?> GetAllStruckInfosAsync(QueryObjectStruckInfomation query)
        {
            var list = _dbContext.StruckInfo.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.customer)) list = list.Where(x => x.customer != null && x.customer.Contains(query.customer));
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
        public async Task<StruckInfo?> GetAllStruckInfosByIdAsync(int id)
        {
            var query = await _dbContext.StruckInfo.FindAsync(id);
            if (query == null) return null;
            return query;
        }

        public async Task<StruckInfo?> UpdateStruckInfo(int id, StruckInfo struckInfo)
        {
            var query = await _dbContext.StruckInfo.FirstOrDefaultAsync(x => x.id == id);
            if (query == null) return null;
            query.carNumber = struckInfo.carNumber;
            query.product = struckInfo.product;
            query.customer = struckInfo.customer;
            query.documents = struckInfo.documents;
            query.isDel = struckInfo.isDel;
            query.notes = struckInfo.notes;
            query.createDate = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            return query;
        }
    }
}
