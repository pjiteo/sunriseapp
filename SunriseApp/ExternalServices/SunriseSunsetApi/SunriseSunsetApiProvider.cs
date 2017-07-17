using Core;
using Newtonsoft.Json;
using RestSharp;
using SunriseApp.ExternalServices.SunriseSunsetApi.Request;
using SunriseApp.ExternalServices.SunriseSunsetApi.Response;
using System.Threading.Tasks;

namespace SunriseApp.ExternalServices.SunriseSunsetApi
{
    public interface ISunriseSunsetApiProvider
    {
        Task<ServiceResult<SunriseSunsetResponse>> GetDayInfo(SunriseSunsetRequest sunriseSunsetRequest);
    }

    public class SunriseSunsetApiProvider : ISunriseSunsetApiProvider
    {
        private readonly string _apiUrl;

        public SunriseSunsetApiProvider()
        {
            _apiUrl = Properties.Settings.Default.sunrisesunsetapi;
        }

        public async Task<ServiceResult<SunriseSunsetResponse>> GetDayInfo(SunriseSunsetRequest sunriseSunsetRequest)
        {
            var result = new ServiceResult<SunriseSunsetResponse>();

            var client = new RestClient(_apiUrl);

            var request = new RestRequest("json");
            request.AddParameter("lat", sunriseSunsetRequest.Latitude);
            request.AddParameter("lng", sunriseSunsetRequest.Longitude);
            request.AddParameter("date", sunriseSunsetRequest.Date.ToString("yyyy-MM-dd"));
            request.AddParameter("formatted", "0");

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var restResponse = await client.ExecuteGetTaskAsync<SunriseSunsetResponse>(request);
            if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                result.Data = JsonConvert.DeserializeObject<SunriseSunsetResponse>(restResponse.Content);
            }
            else
            {
                result.AddError("An error has occurred");
            }

            return result;
        }
    }
}
