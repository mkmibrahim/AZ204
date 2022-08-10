using System;


namespace backend.Models
{
    public class TemperatureInfo : ITemperatureInfo
    {
        public int GetTemperature()
        {
            return 35;
        }
    }
}
