using MUSInovation.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MUSInovation.Service
{
    public static class ChuckNorrisService
    {
        private static readonly string ENDPOINT_URL = "https://api.chucknorris.io/jokes/random";

        public static string GeChuckNorisJoke()
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    var result = webClient.DownloadString(new Uri(ENDPOINT_URL));
                    if (result == null)
                        return "Chuck Norris is too powerful for this connection!";
                    ChuckNorrisModel chuckSays = JsonConvert.DeserializeObject<ChuckNorrisModel>(result);
                    if (chuckSays != null)
                        return chuckSays.value;
                    else
                        return "Chuck Norris is too powerful for this connection!";
                }
            }
            catch (Exception)
            {
                return "Chuck Norris is too powerful for this connection!";
            }
        }
    }
}
