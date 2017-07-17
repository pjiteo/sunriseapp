using NUnit.Framework;
using SunriseApp.ExternalServices.SunriseSunsetApi;
using SunriseApp.ExternalServices.YahooPlace;
using SunriseApp.Services;
using SunriseApp.ViewModels;
using System;

namespace SunriseAppTests
{
    [TestFixture]
    public class MainViewModelTests
    {
        static object[] LocationTestSource =
            {
                new object[] { "49,825401 19,050791", new DateTime(2017, 7, 17), new DateTime(2017, 7, 17, 4, 59, 1), new DateTime(2017, 7, 17, 20, 41, 26) },
                new object[] { "Bielsko-Biała", new DateTime(2017, 7, 17), new DateTime(2017, 7, 17, 4, 59, 1), new DateTime(2017, 7, 17, 20, 41, 26) },
            };

        [TestCaseSource("LocationTestSource")]
        public void When_GetLocation_Expect_ValidResult(string placeInput, DateTime date, DateTime sunrise, DateTime sunset)
        {
            var _dayLengthService = new DayLengthService();
            var _yahooPlaceProvider = new YahooPlaceProvider();
            var _sunriseSunsetApiProvider = new SunriseSunsetApiProvider();
            MainViewModel mainViewModel = new MainViewModel(_dayLengthService, _yahooPlaceProvider, _sunriseSunsetApiProvider);
            mainViewModel.DayInput = date;
            mainViewModel.PlaceInput = placeInput;

            var task = mainViewModel.SearchPlace();
            task.Wait();

            Assert.AreEqual(mainViewModel.Sunrise.Ticks, sunrise.Ticks);
            Assert.AreEqual(mainViewModel.Sunset.Ticks, sunset.Ticks);
        }
    }
}
