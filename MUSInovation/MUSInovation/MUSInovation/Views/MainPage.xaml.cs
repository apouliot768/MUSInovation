using MUSInovation.Service;
using MUSInovation.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MUSInovation
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        // Open the search of movies by title
        private void btnByTitle_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new MovieByTitlePage());
        }

        private void btnByAuthor_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new MoviesByAuthorsPage());
        }

        private void btnRecents_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new UpcomingMoviesPage());
        }

        private void btnHollywoodWeather_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new HollywoodWeatherPage());
        }

        private void btnCuckNorisJoke_Clicked(object sender, EventArgs e)
        {
            string joke = ChuckNorrisService.GeChuckNorisJoke();
            DisplayAlert("Sponsored by chucknorris.io", joke, "OK");
        }

        private async void btnYearFacts_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Enter a year...", "");
            string fact = NumberApiService.GetYearFacts(result);
            await DisplayAlert("Sponsored by numbersapi.com", fact, "OK");
        }
    }
}
