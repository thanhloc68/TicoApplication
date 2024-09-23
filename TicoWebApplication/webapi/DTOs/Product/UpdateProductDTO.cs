namespace webapi.DTOs.Product
{
    public class UpdateProductDTO
    {
        public int? id { get; set; }
        public string? shortcutName { get; set; }
        public string? name { get; set; }
        public bool? status { get; set; }
    }
}
