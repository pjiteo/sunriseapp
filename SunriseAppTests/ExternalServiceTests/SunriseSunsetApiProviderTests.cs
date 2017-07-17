using NUnit.Framework;
using SunriseApp.ExternalServices.SunriseSunsetApi;
using SunriseApp.ExternalServices.SunriseSunsetApi.Response;
using System;

namespace SunriseAppTests.ExternalServiceTests
{
    [TestFixture]
    public class SunriseSunsetApiProviderTests
    {
        private SunriseSunsetApiProvider _sunriseSunsetApiProvider;

        [OneTimeSetUp]
        public void Init()
        {
            _sunriseSunsetApiProvider = new SunriseSunsetApiProvider();
        }

        static object[] SunriseSunsetApiProviderRequestSource =
            {
            new object[] { new SunriseSunsetRequest(36.7201600 , -4.4203400, new DateTime(2017,7,14)), true },
        };

        [TestCaseSource("SunriseSunsetApiProviderRequestSource")]
        public void When_SendRequestToSunriseSunsetApi_Expect_SunriseAndSunsetShouldByTheSameAsExpected(SunriseSunsetRequest sunriseSunsetRequest, bool expected)
        {
            SunriseSunsetApiProvider sunriseSunsetApiProvider = new SunriseSunsetApiProvider();

            var actual = sunriseSunsetApiProvider.GetDayInfo(sunriseSunsetRequest).Result;

            Assert.AreEqual(expected, actual.IsValid);
        }

        [TestCaseSource("SunriseSunsetApiProviderRequestSource")]
        public void When_SendRequest_Expected_NotNllResult(SunriseSunsetRequest sunriseSunsetRequest, bool expected)
        {
            SunriseSunsetApiProvider sunriseSunsetApiProvider = new SunriseSunsetApiProvider();

            var actual = sunriseSunsetApiProvider.GetDayInfo(sunriseSunsetRequest).Result;

            Assert.IsTrue(actual.Data.Results != null);
        }
    }
}
