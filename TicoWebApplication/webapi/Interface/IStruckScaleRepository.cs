using webapi.DTOs.StruckScale;
using webapi.Helpers.QueryStruckScale;
using webapi.Models;

namespace webapi.Interface
{
    public interface IStruckScaleRepository
    {
        Task<List<StruckScales>> GetAllStruckScaleAsync(QueryObjectStruckScale query);
        Task<StruckScales?> GetAllStruckScaleByIdAsync(int id);
        Task<StruckScales?> CreateStruckScale(StruckScales struckInfo);
        Task<StruckScales?> UpdateStruckScale(int id, UpdateStruckScaleDTO struckInfo);
        Task<StruckScales?> DeleteStruckScale(int id);
    }
}
