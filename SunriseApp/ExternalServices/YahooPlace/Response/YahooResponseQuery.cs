using Newtonsoft.Json;
using System.Collections.Generic;

namespace SunriseApp.ExternalServices.YahooPlace.Response
{
    public class YahooResponse
    {
        public YahooResponseQuery Query { get; set; }
    }

    public class YahooResponseResults
    {
        public List<YahooResponsePlace> Place { get; set; }
    }

    public class YahooResponseQuery
    {
        public YahooResponseResults Results { get; set; }
    }

    public class YahooResponseCentroid
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class YahooResponsePlace
    {
        public YahooResponseCentroid Centroid { get; set; }
    }
}
