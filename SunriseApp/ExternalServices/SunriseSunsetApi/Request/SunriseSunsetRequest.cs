using System;

namespace SunriseApp.ExternalServices.SunriseSunsetApi.Response
{
    public class SunriseSunsetRequest
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Date { get; set; }

        public SunriseSunsetRequest(double latitude, double longitude, DateTime date)
        {
            Latitude = latitude;
            Longitude = longitude;
            Date = date;
        }
    }
}
