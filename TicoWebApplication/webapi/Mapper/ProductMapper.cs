using webapi.DTOs.Product;
using webapi.Models;

namespace webapi.Mapper
{
    public static class ProductMapper
    {
        public static ProductInfomationDTO ToProductInfomationDTO(this Product dto)
        {
            return new ProductInfomationDTO
            {
                id = dto.id,
                name = dto.name,
                shortcutName = dto.shortcutName,
                status = dto.status
            };
        }
        public static Product ToCreateProduct(this CreateProductDTO createProduct)
        {
            return new Product
            {
                name = createProduct.name,
                shortcutName = createProduct.shortcutName,
                status = createProduct.status
            };
        }
    }
}
