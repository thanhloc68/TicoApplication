using webapi.DTOs.StruckInfo;
using webapi.Models;
namespace webapi.Mapper
{
    public static class StruckInfoMapper
    {
        public static StruckInfomationDTO ToStruckInfomationDTO(this StruckInfo struckInfo)
        {
            return new StruckInfomationDTO
            {
                id = struckInfo.id,
                carNumber = struckInfo.carNumber,
                createDate = struckInfo.createDate,
                customer = struckInfo.customer,
                documents = struckInfo.documents,
                notes = struckInfo.notes,
                isDel = struckInfo.isDel,
                product = struckInfo.product
            };
        }
        public static StruckInfo ToCreateStruckInfoDTO(this CreateStruckInfomationDTO struckInfo)
        {
            return new StruckInfo
            {
                carNumber = struckInfo.carNumber,
                createDate = struckInfo.createDate,
                customer = struckInfo.customer,
                documents = struckInfo.documents,
                notes = struckInfo.notes,
                isDel = struckInfo.isDel,
                product = struckInfo.product
            };
        }
    }
}
