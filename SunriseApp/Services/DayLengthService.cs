using Core;
using System;

namespace SunriseApp.Services
{
    public interface IDayLengthService
    {
        ServiceResult<DateTime> CalculateDayLength(DateTime start, DateTime end);
    }

    public class DayLengthService : IDayLengthService
    {
        public ServiceResult<DateTime> CalculateDayLength(DateTime start, DateTime end)
        {
            var result = new ServiceResult<DateTime>();
            if (start < end)
            {
                result.Data = new DateTime(end.Ticks - start.Ticks);
            }
            else
            {
                result.AddError("Start hour is greater than end hour");
            }

            return result;
        }
    }
}
