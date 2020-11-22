using MUSInovation.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MUSInovation.Service
{
    public static class NumberApiService
    {
        private static readonly string BEGIN_URL = "http://numbersapi.com/";
        private static readonly string END_URL = "/year?notfound=floor&fragment";

        public static string GetYearFacts(string year)
        {
            try
            {
                int i = 0;

                if (year == null || year == "" || !int.TryParse(year, out i))
                    return "Not a valid year in Gregorian calendar.";

                if (year.Length > 4)
                    return "Probably too far in the future.";

                using (var webClient = new WebClient())
                {
                    var result = webClient.DownloadString(new Uri(BEGIN_URL + year + END_URL));
                    if (result == null)
                        return "Connection Failed!";
                    return result;
                }
            }
            catch (Exception)
            {
                return "Connection Failed!";
            }
        }
    }
}
