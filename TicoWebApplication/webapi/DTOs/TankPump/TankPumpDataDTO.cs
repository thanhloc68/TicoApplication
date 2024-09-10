namespace webapi.DTOs.TankPump
{
    public class TankPumpDataDTO
    {
        public int id { get; set; }
        public int? ordinalNumber { get; set; }
        public string? carNumber { get; set; }
        public string? product { get; set; }
        public string? customer { get; set; }
        public string? documents { get; set; }
        public bool? isDel { get; set; }
        public string? notes { get; set; }
        public string? sourceOfGoods { get; set; }
        public int? requestedVolume { get; set; }
        public int? pumpVolume { get; set; }
        public DateTime? startTimePump { get; set; }
        public DateTime? endTimePump { get; set; }
        public DateTime? createDate { get; set; }
        public int processing { get; set; }
        public int? struckID { get; set; }
    }
}
