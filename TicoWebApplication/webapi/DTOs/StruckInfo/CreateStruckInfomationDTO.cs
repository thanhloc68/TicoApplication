namespace webapi.DTOs.StruckInfo
{
    public class CreateStruckInfomationDTO
    {
        public string? carNumber { get; set; }
        public string? product { get; set; }
        public string? customer { get; set; }
        public string? documents { get; set; }
        public bool? isDel { get; set; }
        public string? notes { get; set; }
        public DateTime? createDate { get; set; }
    }
}
