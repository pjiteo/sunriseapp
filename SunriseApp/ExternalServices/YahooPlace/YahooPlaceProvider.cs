using Core;
using RestSharp;
using SunriseApp.ExternalServices.YahooPlace.Response;
using SunriseApp.Services.Localization;
using System.Linq;
using System.Threading.Tasks;

namespace SunriseApp.ExternalServices.YahooPlace
{
    public interface IYahooPlaceProvider
    {
        Task<ServiceResult<LocalizationModel>> GetPlaceLocalization(string placeName);
    }

    public class YahooPlaceProvider : IYahooPlaceProvider
    {
        private readonly string _yahooPlacesApiUrl;

        public YahooPlaceProvider()
        {
            _yahooPlacesApiUrl = Properties.Settings.Default.yahooplacesapi;
        }

        public async Task<ServiceResult<LocalizationModel>> GetPlaceLocalization(string placeName)
        {
            var result = new ServiceResult<LocalizationModel>();
            if (!string.IsNullOrEmpty(placeName))
            {
                var client = new RestClient(_yahooPlacesApiUrl);

                var request = new RestRequest($"v1/public/yql?");
                request.OnBeforeDeserialization = response => { response.ContentType = "application/json"; };
                request.AddParameter("q", $"select * from geo.places where text=\"{ placeName }\"");
                request.AddParameter("format", "json");

                var restResponse = await client.ExecuteGetTaskAsync<YahooResponse>(request);
                if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var locationResult = GetFirstFittingLocation(restResponse);
                    if (locationResult.IsValid)
                    {
                        result.Data = locationResult.Data;
                    }
                    else
                    {
                        result.AddError(locationResult.Errors.FirstOrDefault());
                    }
                }
                else
                {
                    result.AddError("An error has occurred");
                }
            }
            else
            {
                result.AddError("Place name can't be empty");
            }

            return result;
        }

        private ServiceResult<LocalizationModel> GetFirstFittingLocation(IRestResponse<YahooResponse> restResponse)
        {
            var result = new ServiceResult<LocalizationModel>();

            if (restResponse.Data?.Query?.Results?.Place?.Any() == true)
            {
                var firstLocalization = restResponse.Data?.Query?.Results?.Place?.FirstOrDefault()?.Centroid;
                if (firstLocalization != null)
                {
                    result.Data = new LocalizationModel
                    {
                        Latitude = firstLocalization.Latitude,
                        Longitude = firstLocalization.Longitude
                    };
                }
                else
                {
                    result.AddError("Location not exist");
                }
            }
            else
            {
                result.AddError("Location not exist");
            }

            return result;
        }
    }
}
