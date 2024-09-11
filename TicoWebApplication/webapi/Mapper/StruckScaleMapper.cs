using webapi.DTOs.StruckScale;
using webapi.Models;

namespace webapi.Mapper
{
    public static class StruckScaleMapper
    {
        public static StruckScaleDTO ToStruckScaleDTO(this StruckScales struckScales)
        {
            return new StruckScaleDTO
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
        public static StruckScales ToCreateStruckScales(this CreateStruckScaleDTO struckScales)
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
