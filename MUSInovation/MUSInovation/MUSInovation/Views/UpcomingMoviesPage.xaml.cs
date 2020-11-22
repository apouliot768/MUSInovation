using MUSInovation.Service;
using MUSInovation.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MUSInovation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpcomingMoviesPage : ContentPage
    {
        private static List<TMBdUpcomingViewModel> viewModels { get; set; }
        public UpcomingMoviesPage()
        {
            InitializeComponent();
            _ = GetMovieList();
        }


        // Get upcomings movies in TMBd data base
        private async Task GetMovieList()
        {
            try
            {
                var movies = await TMBdService.GetUpcomingMovies();
                if (movies != null)
                {
                    viewModels = movies;
                    LsvUpcomings.ItemsSource = null;
                    LsvUpcomings.ItemsSource = viewModels;
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

        // Get the movie's official trailer on YouTube
        private async Task GetTrailer(string title, string year)
        {
            try
            {
                string youtubeUrl = await YouTubeService.GeYoutubeTrailer(title, year);
                if (youtubeUrl != null)
                    WebYoutube.Source = youtubeUrl;
                else
                    WebYoutube.Source = "";
            }
            catch (Exception)
            {
                WebYoutube.Source = "";
            }
            waitLayout2.IsVisible = false;
            WebYoutube.IsVisible = true;
        }

        private void FailedToGetUpcommings()
        {
            viewModels = null;
            LblError.Text = "Connection failed...";
        }

        private void btnCloseYoutube_Clicked(object sender, EventArgs e)
        {
            WebYoutube.IsVisible = false;
            popuplayout.IsVisible = false;
            WebYoutube.Source = "https://www.youtube.com/";
        }

        private void LsvUpcomings_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            TMBdUpcomingViewModel viewModel = LsvUpcomings.SelectedItem as TMBdUpcomingViewModel;
            if (viewModel != null)
            {
                LsvUpcomings.SelectedItem = null;
                popuplayout.IsVisible = true;
                waitLayout2.IsVisible = true;
                string year = viewModel.release_date.Substring(0, 4);
                _ = GetTrailer(viewModel.title, year);
            }
        }
    }
}