﻿namespace webapi.DTOs.StruckScale
{
    public class CreateStruckScaleDTO
    {
        public double? firstScale { get; set; }
        public double? secondScale { get; set; }
        public double? results { get; set; }
        public DateTime? firstScaleDate { get; set; }
        public DateTime? secondScaleDate { get; set; }
        public DateTime? createDate { get; set; }
        public string? styleScale { get; set; }
        public bool? isDone { get; set; }
        public int? struckID { get; set; }
    }
}
