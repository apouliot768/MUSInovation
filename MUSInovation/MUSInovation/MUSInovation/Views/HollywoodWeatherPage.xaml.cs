using MUSInovation.Service;
using MUSInovation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MUSInovation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HollywoodWeatherPage : ContentPage
    {
        private static List<WeatherGovForcastViewModel> viewModels { get; set; }
        public HollywoodWeatherPage()
        {
            InitializeComponent();
            waitLayout.IsVisible = true;
            _ = GetWeather();
        }

        private async Task GetWeather()
        {
            try
            {
                var weather = await WeatherGovService.GetHollywoodWeather();
                if (weather != null)
                {
                    viewModels = weather;
                    LsvWeather.ItemsSource = null;
                    LsvWeather.ItemsSource = viewModels;
                    LblError.Text = "";
                }
                else
                {
                    FailedToGetUpcommings();
                }
            }
            catch (Exception)
            {
                FailedToGetUpcommings();
            }
            waitLayout.IsVisible = false;
        }

        private void FailedToGetUpcommings()
        {
            viewModels = null;
            LblError.Text = "Connection failed...";
        }
    }
}