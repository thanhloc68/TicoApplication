namespace webapi.Helpers.QueryStruckInfomation
{
    public class QueryObjectStruckInfomation
    {
        public string? carNumber { get; set; } = null;
        public string? product { get; set; } = null;
        public string? customer { get; set; } = null;
        public string? documents { get; set; } = null;
        public DateTime? createDate { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool isDecsending { get; set; } = false;
    }
}
