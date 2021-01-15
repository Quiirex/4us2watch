using _4us2watch.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using _4us2watch.Data;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Windows.Input;
using _4us2watch.Components;

namespace _4us2watch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GenreAssignmentPage : ContentPage
    {
        int counter = 1;
        // Main api for movies
        public static string MainApi = @"https://api.themoviedb.org/3/movie/popular?api_key=9d2bff12ed955c7f1f74b83187f188ae&language=en-US&page=";
        // Base URL for Image
        public static string ImageLink = @"https://image.tmdb.org/t/p/w500";
        public string email;
        public List<string> movieList = new List<string>();
        public Queue<Movie> MoviesQueue;
        private Movie CurrentMovie;
        public GenreAssignmentPage(string text)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            GetFirstMovies();
            ChangeElements();
            email = text;

        }
        private void ChangeElements()
        {
            CurrentMovie = MoviesQueue.Dequeue();
            Poster.Source = ImageLink + "/" + CurrentMovie.ImagePath;
            Setup.Text = "Setting up your app (" + counter + "/20)";
            Title.Text = CurrentMovie.Name + " (" + CurrentMovie.ReleaseDate.Substring(0, 4) + ")";
        }
        async void DislikeBtn(object sender, EventArgs args)
        {
            var user = await ReaderWriter.GetPerson(email);

            try
            {
                ++counter;
                if (MoviesQueue.Count == 0)
                {
                    await ReaderWriter.UpdatePerson(user.username, user.email, user.friends, movieList);
                    await Navigation.PushAsync(new GridPage(user.email));
                    return;
                    // Here is where it will end
                    // The ratings will be implemented when the databse support will be as well
                }
                ChangeElements();
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", "There has been an unexcpected ERROR please try again later", "OK");
            }
        }
        async void LikeBtn(object sender, EventArgs args)
        {
            var user = await ReaderWriter.GetPerson(email);
            //Implement like
            //DisplayAlert("JE VŠEČ", "NAJS", "OK");

            try
            {
                movieList.Add(CurrentMovie.idMovie.ToString());
                ++counter;
                if (MoviesQueue.Count == 0)
                {
                    await ReaderWriter.UpdatePerson(user.username, user.email, user.friends, movieList);
                    await Navigation.PushAsync(new GridPage(user.email));
                    return;
                    // Here is where it will end
                    // The ratings will be implemented when the databse support will be as well
                }
                ChangeElements();
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", "There has been an unexcpected ERROR please try again later", "OK");
            }


        }

        private void GetFirstMovies()
        {
            var random = new Random();
            var pageNumber = random.Next(1, 501);

            // API call
            var client = new HttpClient();
            client.BaseAddress = new Uri(MainApi);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync(MainApi + pageNumber.ToString()).Result; // Main result for API
            if (response.IsSuccessStatusCode)
            {
                var convertedString = response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<MoviePage>(convertedString.Result);
                MoviesQueue = new Queue<Movie>(data.Results); // Fiting movies to queue

                //ReplaceElements(Database.Movies[Counter]);
            }
            else
            {
                DisplayAlert("Error", "The api did not return a success status code", "OK");
            }
        }

        //private void GetGenres()
        //{
        //    clientGenre.BaseAddress = new Uri(genreApiUrl);
        //    clientGenre.DefaultRequestHeaders.Accept.Clear();
        //    clientGenre.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    var response = clientGenre.GetAsync(genreApiUrl).Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var convertedString = response.Content.ReadAsStringAsync();
        //        var data = JsonConvert.DeserializeObject<GenreDummyClass>(convertedString.Result);
        //        Database.Genres = data.Genres.ToList();
        //    }
        //    else
        //    {
        //        MessageBox.Show("The api did not return a success status code");
        //    }
        //}
        protected override bool OnBackButtonPressed() => true; //da ne more backoutat, ker se ruši navigacija
    }
}
