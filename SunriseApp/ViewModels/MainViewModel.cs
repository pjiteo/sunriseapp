using System;
using SunriseApp.Services;
using PropertyChanged;
using System.Windows.Input;
using Core;
using System.Text.RegularExpressions;
using SunriseApp.ExternalServices.YahooPlace;
using SunriseApp.ExternalServices.SunriseSunsetApi;
using SunriseApp.ExternalServices.SunriseSunsetApi.Response;
using SunriseApp.Services.Localization;
using System.Threading.Tasks;

namespace SunriseApp.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModel
    {
        public string PlaceInput { get; set; }
        public DateTime DayInput { get; set; }

        private readonly IDayLengthService _dayLengthService;
        private readonly ISunriseSunsetApiProvider _sunriseSunsetApiProvider;
        private readonly IYahooPlaceProvider _yahooPlaceProvider;

        private ICommand _searchCommand;
        public ICommand SearchCommand { get { return _searchCommand ?? (_searchCommand = new CommandHandler(async () => await SearchPlace(), true)); } }

        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }
        public string GetSunrise => Sunrise.ToString("HH:mm:ss");
        public string GetSunset => Sunset.ToString("HH:mm:ss");
        public string GetDayLength
        {
            get
            {
                var result = _dayLengthService.CalculateDayLength(Sunrise, Sunset);
                if (result.IsValid)
                {
                    return $"{result.Data.Hour} godzin {result.Data.Minute} minut {result.Data.Second} sekund";
                }
                return "Error";
            }
        }

        public MainViewModel(IDayLengthService dayLengthService, IYahooPlaceProvider yahooPlaceProvider, ISunriseSunsetApiProvider sunriseSunsetApiProvider)
        {
            _dayLengthService = dayLengthService;
            _yahooPlaceProvider = yahooPlaceProvider;
            _sunriseSunsetApiProvider = sunriseSunsetApiProvider;
            DayInput = DateTime.Now;
        }

        private async Task<ServiceResult<LocalizationModel>> GetLocalizationFromInput(string input)
        {
            var result = new ServiceResult<LocalizationModel>();
            if (!string.IsNullOrEmpty(PlaceInput))
            {
                MatchCollection matches = Regex.Matches(PlaceInput, @"-{0,1}\d{1,3}\.\d{1,6}");
                if (matches.Count == 2)
                {
                    double lat;
                    double lng;
                    if (double.TryParse(matches[0].Value, out lat) && double.TryParse(matches[1].Value, out lng))
                    {
                        result.Data = new LocalizationModel
                        {
                            Latitude = lat,
                            Longitude = lng
                        };
                    }
                    else
                    {
                        result.AddError("Cannot find place");
                    }
                }
                else
                {
                    var yahooServiceResult = await _yahooPlaceProvider.GetPlaceLocalization(PlaceInput);
                    if (result.IsValid)
                    {
                        result.Data = new LocalizationModel
                        {
                            Latitude = yahooServiceResult.Data.Latitude,
                            Longitude = yahooServiceResult.Data.Longitude
                        };
                    }
                    else
                    {
                        result.AddError("Cannot find place");
                    }
                }
            }
            else
            {
                result.AddError("Cannot find place");
            }

            return result;
        }

        public async Task SearchPlace()
        {
            var result = await GetLocalizationFromInput(PlaceInput);
            if (result.IsValid)
            {
                var request = new SunriseSunsetRequest(result.Data.Latitude, result.Data.Longitude, DayInput);
                var sunriseSunsetResult = await _sunriseSunsetApiProvider.GetDayInfo(request);
                if (sunriseSunsetResult.IsValid)
                {
                    Sunset = sunriseSunsetResult.Data.Results.Sunset;
                    Sunrise = sunriseSunsetResult.Data.Results.Sunrise;
                }
            }
        }
    }
}
