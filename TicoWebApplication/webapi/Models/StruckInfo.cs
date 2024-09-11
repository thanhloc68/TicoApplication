
namespace webapi.Models
{
    public class StruckInfo
    {
        public int? id { get; set; }
        public string? carNumber { get; set; }
        public string? product { get; set; }
        public string? customer { get; set; }
        public string? documents { get; set; }
        public bool? isDel { get; set; }
        public string? notes { get; set; }
        public DateTime? createDate { get; set; }
        public virtual ICollection<StruckScales>? StruckScales { get; set; }
        public virtual ICollection<TankStrucks>? TankStrucks { get; set; }
    }
}
