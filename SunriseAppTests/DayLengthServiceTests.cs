using NUnit.Framework;
using SunriseApp.Services;
using System;

namespace SunriseAppTests
{
    [TestFixture]
    public class DayLengthServiceTests
    {
        static object[] DayLengthSource =
            {
                new object[] { new DateTime(2000, 1, 1, 1, 10, 15), new DateTime(2000, 1, 1, 2, 10, 15), new DateTime(new DateTime(2000, 1, 1, 2, 10, 15).Ticks - new DateTime(2000, 1, 1, 1, 10, 15).Ticks)},
                new object[] { new DateTime(2000, 1, 1, 0, 0, 0), new DateTime(2000, 1, 1, 2, 10, 15), new DateTime(new DateTime(2000, 1, 1, 2, 10, 15).Ticks - new DateTime(2000, 1, 1, 0, 0, 0).Ticks) }
            };

        [TestCaseSource("DayLengthSource")]
        public void When_CalculatingLengthOfTwoDates_Expect_ValidLengthOfDay(DateTime start, DateTime end, DateTime expected)
        {
            DayLengthService dayLengthService = new DayLengthService();

            var actual = dayLengthService.CalculateDayLength(start, end);

            Assert.AreEqual(expected.Ticks, actual.Data.Ticks);
        }

        [TestCase(10, 1, false)]
        public void When_StartDateIsGreaterThanEndDate_ExpectInValidServiceResult(int startHour, int endHour, bool expected)
        {
            DayLengthService dayLengthService = new DayLengthService();

            var actual = dayLengthService.CalculateDayLength(new DateTime(2000, 1, 1, startHour, 0, 0), new DateTime(2000, 1, 1, endHour, 0, 0));

            Assert.AreEqual(expected, actual.IsValid);
        }
    }
}
