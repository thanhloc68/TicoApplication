using Microsoft.EntityFrameworkCore;
using System;
using webapi.Data;
using webapi.DTOs.StruckScale;
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

        public async Task<List<StruckScales>> GetAllStruckScaleAsync(QueryObjectStruckScale query)
        {
            DateTime dateTime = DateTime.Now;
            DateTime DayNow = dateTime.AddDays(-1);
            var list = _dbContext.StruckScale
                .Include(x => x.StruckInfo)
                .AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.styleScale)) list = list.Where(x => x.styleScale != null && x.styleScale.Contains(query.styleScale));
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                list = query.SortBy.ToLower() switch
                {
                    "id" => query.isDecsending ? list.OrderByDescending(s => s.id) : list.OrderBy(s => s.id),
                    _ => list // Nếu SortBy không khớp với bất kỳ giá trị nào, không thay đổi thứ tự
                };
            }
            list = list.Where(x => x.createDate >= DayNow || x.secondScale == 0);
            var data = await list.ToListAsync();
            return data;
        }

        public async Task<StruckScales?> GetAllStruckScaleByIdAsync(int id)
        {
            var query = await _dbContext.StruckScale.FindAsync(id);
            if (query == null) return null;
            return query;
        }

        public async Task<StruckScales?> UpdateStruckScale(int id, UpdateStruckScaleDTO struckInfo)
        {
            var query = await _dbContext.StruckScale.FirstOrDefaultAsync(x => x.id == id);
            if (query == null) return null;
            if (query.firstScale == 0)
            {
                query.firstScale = struckInfo.firstScale;
                query.firstScaleDate = DateTime.Now;
                query.isDone = false;
            }
            if (query.secondScale == 0)
            {
                query.secondScale = struckInfo.secondScale;
                query.results = struckInfo.firstScale - struckInfo.secondScale;
                if (query.results != null && query.results > 0)
                {
                    query.styleScale = "Nhập hàng";
                }
                else
                {
                    query.styleScale = "Xuất hàng";
                    if (query.results.HasValue) query.results = Math.Abs((double)query.results);
                }
                query.secondScaleDate = DateTime.Now;
            }
            await _dbContext.SaveChangesAsync();
            return query;
        }
    }
}
