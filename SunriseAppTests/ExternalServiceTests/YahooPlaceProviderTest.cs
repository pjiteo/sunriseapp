using NUnit.Framework;
using SunriseApp.ExternalServices.YahooPlace;

namespace SunriseAppTests.ExternalServiceTests
{
    [TestFixture]
    public class YahooPlaceProviderTest
    {
        static object[] SunriseSunsetApiProviderRequestSource =
            {
                new object[] { "Bielsko-Biala", 49.825401, 19.050791 },
                new object[] { "chicago", 41.884151, -87.632408 },
                new object[] { "chIcago", 41.884151, -87.632408 },
                new object[] { "CHICAGO", 41.884151, -87.632408 },
        };

        [TestCaseSource("SunriseSunsetApiProviderRequestSource")]
        public void When_SendRequestToYahooPlaceApi_Expected_GetValidLocation(string placeName, double latitude, double longitude)
        {
            YahooPlaceProvider yahooPlaceProvider = new YahooPlaceProvider();

            var actual = yahooPlaceProvider.GetPlaceLocalization(placeName).Result;

            Assert.AreEqual(latitude, actual.Data.Latitude);
            Assert.AreEqual(longitude, actual.Data.Longitude);
        }
    }
}
