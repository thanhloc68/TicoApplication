using webapi.Helpers.QueryStruckInfomation;
using webapi.Models;

namespace webapi.Interface
{
    public interface IStuckInfomationRepository
    {
        Task<List<StruckInfo>?> GetAllStruckInfosAsync(QueryObjectStruckInfomation query);
        Task<StruckInfo?> GetAllStruckInfosByIdAsync(int id);
        Task<StruckInfo?> CreateStruckInfo(StruckInfo struckInfo);
        Task<StruckInfo?> UpdateStruckInfo(int id, StruckInfo struckInfo);
        Task<StruckInfo?> DeleteStruckInfo(int id);
    }
}
