using MUSInovation.Model.Youtube.LongSearch;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MUSInovation.Service
{
    /// <summary>
    /// Author :        Alexandre Pouliot
    /// Description :   This class is responsible for getting data from the YouTube API.
    /// Date :          2020-11-19
    /// </summary>
    public static class YouTubeService
    {
        private static readonly string API_KEY = "AIzaSyDOeLq0eXhoe5UUP_gVFPq1miEK_fys0Bs";
        private static readonly string ENDPOINT_URL = "https://www.googleapis.com/youtube/v3/search?part=snippet&q=";
        private static readonly string INNER_RQUEST = "official trailer&type=video&videoCaption=closedCaption&key=";
        private static readonly string YOUTUBE_RQUEST = "https://www.youtube.com/watch?v=";

        public static async Task<string> GeYoutubeTrailer(string title, string year)
        {
            if (title == null)
                return "";
            if (year == null)
                year = "";

            var url = ENDPOINT_URL + title + " " + year + " " + INNER_RQUEST + API_KEY;

            try
            {
                using (var webClient = new WebClient())
                {
                    var result = await Task.Run(() => webClient.DownloadString(new Uri(url)));
                    if (result == null)
                        return "https://www.youtube.com/results?search_query=" + title + " " + year + " official trailer";
                    YoutubeLongSearchModel youtubeSearch = JsonConvert.DeserializeObject<YoutubeLongSearchModel>(result);

                    if (youtubeSearch != null && youtubeSearch.items.Count > 0)
                    {
                        string goodHash = "";
                        bool findTitle = false;
                        foreach (Item item in youtubeSearch.items)
                        {
                            if (item.snippet.title.Contains(title) && findTitle == false)
                            {
                                goodHash = item.id.videoId;
                                findTitle = true;
                            }
                        }

                        if (goodHash == "")
                        {
                            return "https://www.youtube.com/results?search_query=" + title + " " + year + " official trailer";
                        }

                        return YOUTUBE_RQUEST + goodHash;
                    }
                    else
                        return "https://www.youtube.com/results?search_query=" + title + " " + year + " official trailer";
                }
            }
            catch (Exception)
            {
                return "https://www.youtube.com/results?search_query=" + title + " " + year + " official trailer";
            }
        }
    }
}
