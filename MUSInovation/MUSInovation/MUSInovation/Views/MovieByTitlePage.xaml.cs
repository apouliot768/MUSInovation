using MUSInovation.Service;
using MUSInovation.ViewModel;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MUSInovation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieByTitlePage : ContentPage
    {
        private OMBdByTitleViewModel viewModel { get; set; }
        private string YoutubeTrailerUrl { get; set; }

        public MovieByTitlePage()
        {
            InitializeComponent();
            if (viewModel == null)
            {
                viewModel = new OMBdByTitleViewModel();
                viewModel.Title = "Search something...";
                ImgPoster.Source = "iconMI.png";
            }

            if (viewModel.Ratings != null)
                LsvOMDb.HeightRequest = (viewModel.Ratings.Count * 45);

            waitLayout.IsVisible = false;
            waitLayout2.IsVisible = false;
            this.BindingContext = viewModel;
            WebYoutube.Source = "";
        }

        private void btnByTitle_Clicked(object sender, EventArgs e)
        {
            waitLayout.IsVisible = true;
            _ = GetMovieList();
        }

        private async Task GetMovieList()
        {
            try
            {
                var movie = await OMBdService.GetMovieByTitle(edtMovieTitle.Text);
                if (movie != null)
                {
                    viewModel = movie;
                    this.BindingContext = viewModel;
                    if (viewModel.Poster == null)
                        ImgPoster.Source = "iconMI.png";
                    else
                        ImgPoster.Source = viewModel.Poster;
                    if (viewModel.Ratings != null)
                        LsvOMDb.HeightRequest = (viewModel.Ratings.Count * 45);
                }
                else
                {
                    viewModel = new OMBdByTitleViewModel();
                    viewModel.Title = "No results...";
                    this.BindingContext = viewModel;
                    ImgPoster.Source = "iconMI.png";
                }
            }
            catch (Exception)
            {
                viewModel = new OMBdByTitleViewModel();
                viewModel.Title = "Check your internet connection...";
                this.BindingContext = viewModel;
                ImgPoster.Source = "iconMI.png";
            }
            waitLayout.IsVisible = false;
        }

        // Get the movie's official trailer on YouTube
        private async Task GetTrailer()
        {
            try
            {
                string youtubeUrl = await YouTubeService.GeYoutubeTrailer(viewModel.Title, viewModel.Year);
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
            popuplayout.IsVisible = true;
        }

        private void btnLoadYoutube_Clicked(object sender, EventArgs e)
        {
            waitLayout2.IsVisible = true;
            _ = GetTrailer();
        }

        private void btnCloseYoutube_Clicked(object sender, EventArgs e)
        {
            popuplayout.IsVisible = false;
            WebYoutube.Source = "https://www.youtube.com/";
        }
    }
}