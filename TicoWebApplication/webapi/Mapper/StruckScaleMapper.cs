using webapi.DTOs.StruckScale;
using webapi.Models;

namespace webapi.Mapper
{
    public static class StruckScaleMapper
    {
        public static StruckScaleDataDTO ToStruckScaleDataDTO(this StruckScales struckScales)
        {
            return new StruckScaleDataDTO
            {
                id = struckScales.id,
                firstScale = struckScales.firstScale,
                firstScaleDate = struckScales.firstScaleDate,
                secondScale = struckScales.secondScale,
                secondScaleDate = struckScales.secondScaleDate,
                results = struckScales.results,
                styleScale = struckScales.styleScale,
                createDate = struckScales.createDate,
                isDone = struckScales.isDone,
            };
        }
        public static StruckScales ToCreateStruckScales(this CreateStruckScaleDataDTO struckScales)
        {
            return new StruckScales
            {
                firstScale = struckScales.firstScale,
                firstScaleDate = struckScales.firstScaleDate,
                secondScale = struckScales.secondScale,
                secondScaleDate = struckScales.secondScaleDate,
                results = struckScales.results,
                styleScale = struckScales.styleScale,
                createDate = struckScales.createDate,
                isDone = struckScales.isDone,
            };
        }
    }
}
