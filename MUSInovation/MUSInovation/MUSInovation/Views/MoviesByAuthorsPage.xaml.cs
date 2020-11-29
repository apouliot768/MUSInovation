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
    public partial class MoviesByAuthorsPage : ContentPage
    {
        private static List<TMBdByAuthorsViewModel> viewModels { get; set; }

        public MoviesByAuthorsPage()
        {
            InitializeComponent();
            waitLayout.IsVisible = false;
        }

        // Get upcomings movies in TMBd data base
        private async Task GetMovieList()
        {
            try
            {
                var movies = await TMBdService.GetAuthorMovies(edtMovieAuthor.Text);
                if (movies != null && movies.Count > 0)
                {
                    viewModels = movies;
                    LsvByAuthors.ItemsSource = null;
                    LsvByAuthors.ItemsSource = viewModels;
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
            LblError.Text = "Sorry Not found...";
        }

        private void btnCloseYoutube_Clicked(object sender, EventArgs e)
        {
            WebYoutube.IsVisible = false;
            popuplayout.IsVisible = false;
            WebYoutube.Source = "https://www.youtube.com/";
        }

        private void LsvUpcomings_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            TMBdByAuthorsViewModel viewModel = LsvByAuthors.SelectedItem as TMBdByAuthorsViewModel;
            if (viewModel != null)
            {
                LblError.Text = "";
                LsvByAuthors.SelectedItem = null;
                popuplayout.IsVisible = true;
                waitLayout2.IsVisible = true;
                string year = viewModel.release_date.Substring(0, 4);
                _ = GetTrailer(viewModel.title, year);
            }
        }

        private void btnByAuthor_Clicked(object sender, EventArgs e)
        {
            waitLayout.IsVisible = true;
            _ = GetMovieList();
        }
    }
}