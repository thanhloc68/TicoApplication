using webapi.DTOs.StruckInfo;
using webapi.Helpers.QueryStruckInfomation;
using webapi.Models;

namespace webapi.Interface
{
    public interface IStruckInfomationRepository
    {
        Task<List<StruckInfo>> GetAllStruckInfosAsync(QueryObjectStruckInfomation query);
        Task<StruckInfo?> GetAllStruckInfosByIdAsync(int id);
        Task<StruckInfo?> CreateStruckInfo(StruckInfo struckInfo);
        Task<StruckInfo?> UpdateStruckInfo(int id, UpdateInfomationDTO updateInfomation);
        Task<StruckInfo?> DeleteStruckInfo(int id);
    }
}
