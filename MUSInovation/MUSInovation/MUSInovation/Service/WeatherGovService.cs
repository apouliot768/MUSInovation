using MUSInovation.Model;
using MUSInovation.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MUSInovation.Service
{
    public static class WeatherGovService
    {
        private static readonly string ENDPOINT_URL = "https://api.weather.gov/gridpoints/LOX/152,46/forecast";

        public static async Task<List<WeatherGovForcastViewModel>> GetHollywoodWeather()
        {

            try
            {
                using (var webClient = new WebClient())
                {
                    webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; " +
                                  "Windows NT 5.2; .NET CLR 1.0.3705;)");
                    var result = await Task.Run(() => webClient.DownloadString(new Uri(ENDPOINT_URL)));
                    if (result == null)
                        return null;
                    WeatherGovForcastModel weather = JsonConvert.DeserializeObject<WeatherGovForcastModel>(result);

                    if (weather != null)
                    {
                        List<WeatherGovForcastViewModel> viewModels = new List<WeatherGovForcastViewModel>();

                        foreach (var item in weather.properties.periods)
                        {
                            viewModels.Add(new WeatherGovForcastViewModel()
                            {
                                detailedForecast = item.detailedForecast,
                                icon = new Uri(item.icon),
                                name = item.name
                            });
                        }

                        return viewModels;
                    }
                    else
                        return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
