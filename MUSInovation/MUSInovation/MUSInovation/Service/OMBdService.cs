using MUSInovation.Model;
using MUSInovation.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MUSInovation.Service
{
    /// <summary>
    /// Author :        Alexandre Pouliot
    /// Description :   This class is responsible for getting data from the OMBd API.
    /// Date :          2020-11-18
    /// </summary>
    public static class OMBdService
    {
        private static readonly string API_KEY = "&apikey=c0db3787";
        private static readonly string ENDPOINT_URL = "http://www.omdbapi.com/";
        public static async Task<OMBdByTitleViewModel> GetMovieByTitle(string title)
        {
            if (title == null)
                return null;

            var url = ENDPOINT_URL + "?t=" + title + API_KEY;

            try
            {
                using (var webClient = new WebClient())
                {
                    var result = await Task.Run(() => webClient.DownloadString(new Uri(url)));
                    if (result == null)
                        return null;
                    OMBdModel OMBdmovie = JsonConvert.DeserializeObject<OMBdModel>(result);

                    if (OMBdmovie.Type == "movie")
                    {
                        OMBdByTitleViewModel viewModel = new OMBdByTitleViewModel(OMBdmovie);
                        return viewModel;
                    }
                    else
                        return MovieNotFound();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static OMBdByTitleViewModel MovieNotFound()
        {
            return new OMBdByTitleViewModel() { Title = "No results..." };
        }
    }
}
