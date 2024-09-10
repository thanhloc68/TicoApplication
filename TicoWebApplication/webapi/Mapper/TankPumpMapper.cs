using webapi.DTOs.TankPump;
using webapi.Models;

namespace webapi.Mapper
{
    public static class TankPumpMapper
    {
        public static TankPumpDataDTO ToTankPumpDataDTO(this TankStrucks tankPumpData)
        {
            return new TankPumpDataDTO
            {
                id = tankPumpData.id,
                processing = tankPumpData.processing,
                pumpVolume = tankPumpData.pumpVolume,
                requestedVolume = tankPumpData.requestedVolume,
                sourceOfGoods = tankPumpData.sourceOfGoods,
                startTimePump = tankPumpData.startTimePump,
                struckID = tankPumpData.struckID,
                endTimePump = tankPumpData.endTimePump,
                createDate = tankPumpData.createDate,
            };
        }
        public static TankStrucks ToCreateTankStrucks(this CreateTankPumpDataDTO tankPumpData)
        {
            return new TankStrucks
            {
                processing = tankPumpData.processing,
                pumpVolume = tankPumpData.pumpVolume,
                requestedVolume = tankPumpData.requestedVolume,
                sourceOfGoods = tankPumpData.sourceOfGoods,
                startTimePump = tankPumpData.startTimePump,
                struckID = tankPumpData.struckID,
                endTimePump = tankPumpData.endTimePump,
                createDate = tankPumpData.createDate,
            };
        }
    }
}
