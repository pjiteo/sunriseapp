using Autofac;
using SunriseApp.ExternalServices.SunriseSunsetApi;
using SunriseApp.ExternalServices.YahooPlace;
using SunriseApp.Services;
using System.Windows;

namespace SunriseApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DayLengthService>().As<IDayLengthService>();
            builder.RegisterType<YahooPlaceProvider>().As<IYahooPlaceProvider>();
            builder.RegisterType<SunriseSunsetApiProvider>().As<ISunriseSunsetApiProvider>();
            builder.RegisterType<MainWindow>();

            var container = builder.Build();
            container.Resolve<MainWindow>().Show();
        }
    }
}
