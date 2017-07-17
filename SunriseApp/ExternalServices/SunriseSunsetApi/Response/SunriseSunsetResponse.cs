using Newtonsoft.Json;
using System;

namespace SunriseApp.ExternalServices.SunriseSunsetApi.Request
{
    public class SunriseSunsetResponseResult
    {
        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }
    }

    public class SunriseSunsetResponse
    {
        public SunriseSunsetResponseResult Results { get; set; }
        public string Status { get; set; }
    }
}
