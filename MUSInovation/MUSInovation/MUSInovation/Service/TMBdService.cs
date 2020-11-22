using MUSInovation.Model.TMBd.TMBdUpcoming;
using MUSInovation.Model.TMBd.TMBdAuthor;
using MUSInovation.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MUSInovation.Service
{
    /// <summary>
    /// Author :        Alexandre Pouliot
    /// Description :   This class is responsible for getting data from the TMBd API.
    /// Date :          2020-11-18
    /// </summary>
    public class TMBdService
    {
        private static readonly string UPCOMING_URL = "https://api.themoviedb.org/3/movie/upcoming?api_key=03cfe921492a01d21b584ee925baa7f6&language=en-US&page=";
        private static readonly string IMAGE_URL = "https://image.tmdb.org/t/p/w185/";
        private static readonly string AUTHOR_BRGIN_URL = "https://api.themoviedb.org/3/search/person?query=";
        private static readonly string AUTHOR_END_URL = "&api_key=03cfe921492a01d21b584ee925baa7f6&language=en-US";

        public static async Task<List<TMBdUpcomingViewModel>> GetUpcomingMovies()
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    List<TMBdUpcomingViewModel> viewModels = new List<TMBdUpcomingViewModel>();
                    for (int i = 1; i < 5; i++)
                    {
                        var upcomings = await Task.Run(() => webClient.DownloadString(new Uri(UPCOMING_URL + i.ToString())));
                        if (upcomings != null)
                        {
                            TMBdUpcoming TMBdUpcomings = JsonConvert.DeserializeObject<TMBdUpcoming>(upcomings);
                            if (TMBdUpcomings != null)
                            {
                                viewModels.AddRange(TMBdUpcomings.results.Select(x => new TMBdUpcomingViewModel()
                                {
                                    original_language = x.original_language,
                                    overview = x.overview,
                                    poster_path = new Uri(IMAGE_URL + x.poster_path),
                                    release_date = x.release_date,
                                    title = x.title,
                                    vote_average = x.vote_average
                                }).ToList());
                            }
                        }
                    }
                    DateTime date = DateTime.Now;
                    string currentYear = date.ToString("yyyy");
                    date = date.AddYears(1);
                    string nextYear = date.ToString("yyyy");
                    viewModels = viewModels.Where(x => x.release_date.Contains(currentYear) || x.release_date.Contains(nextYear)).ToList();
                    viewModels = viewModels.OrderByDescending(x => x.release_date).ToList();
                    return viewModels;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task<List<TMBdByAuthorsViewModel>> GetAuthorMovies(string givenName)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    List<TMBdByAuthorsViewModel> viewModels = new List<TMBdByAuthorsViewModel>();

                    var upcomings = await Task.Run(() => webClient.DownloadString(new Uri(AUTHOR_BRGIN_URL + givenName + AUTHOR_END_URL)));
                    if (upcomings != null)
                    {
                        TMBdByAuthorModel TMBdUpcomings = JsonConvert.DeserializeObject<TMBdByAuthorModel>(upcomings);
                        if (TMBdUpcomings != null)
                        {
                            foreach (var result in TMBdUpcomings.results)
                            {
                                foreach (var item in result.known_for)
                                {
                                    viewModels.Add(new TMBdByAuthorsViewModel()
                                    {
                                        original_language = item.original_language,
                                        overview = item.overview,
                                        poster_path = new Uri(IMAGE_URL + item.poster_path),
                                        release_date = item.release_date,
                                        title = item.title,
                                        vote_average = item.vote_average,
                                        known_for_department = result.known_for_department,
                                        name = result.name
                                    });
                                }
                            }
                        }
                    }

                    viewModels = viewModels.Where(x => x.known_for_department == "Directing").ToList();
                    return viewModels;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
