using MahApps.Metro.Controls;
using SunriseApp.ExternalServices.SunriseSunsetApi;
using SunriseApp.ExternalServices.YahooPlace;
using SunriseApp.Services;
using SunriseApp.ViewModels;
using System.Windows;

namespace SunriseApp
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow(
            IDayLengthService dayLengthService, 
            IYahooPlaceProvider yahooPlaceProvider, 
            ISunriseSunsetApiProvider sunriseSunsetApiProvider)
        {
            InitializeComponent();
            DataContext = new MainViewModel(dayLengthService, yahooPlaceProvider, sunriseSunsetApiProvider);
        }
    }
}
