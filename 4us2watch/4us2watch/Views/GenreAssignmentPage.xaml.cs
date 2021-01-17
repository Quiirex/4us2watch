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
using Rg.Plugins.Popup.Services;

namespace _4us2watch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GenreAssignmentPage : ContentPage
    {
        int likecounter = 0;
        int counter = 1;
        // Main api for movies
        public static string MainApi = @"https://api.themoviedb.org/3/movie/popular?api_key=9d2bff12ed955c7f1f74b83187f188ae&language=en-US&page=";
        // Base URL for Image
        public static string ImageLink = @"https://image.tmdb.org/t/p/w500";
        public string email;
        public List<string> movieList = new List<string>();
        public Queue<Movie> MoviesQueue;
        private Movie CurrentMovie;
        GenreAssignmentPage restart = null;
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
                    await PopupNavigation.Instance.PushAsync(new BusyPopUp());

                    try
                    {
                        if(likecounter == 0)
                        {
                            await DisplayAlert("Thats awkward", "You didn't like any movies, well time to start over with fresh movies.", "OK");
                            if (restart == null)
                            {
                                restart = new GenreAssignmentPage(user.email);
                            }
                            App.Current.MainPage = new NavigationPage(restart);

                        }
                        else if(likecounter < 10)
                        {
                            bool decision = await DisplayAlert("Thats awkward", "You only liked less than 10 movies would you like to start over?", "Yes", "No");
                            if (decision == true)
                            {
                                if(restart == null)
                                {
                                    restart = new GenreAssignmentPage(user.email);   
                                }
                                App.Current.MainPage = new NavigationPage(restart);
                            }
                            else
                            {
                                await ReaderWriter.UpdatePerson(user.username, user.email, user.friends, movieList);
                                await Navigation.PushAsync(new GridPage(user.email));
                                return;
                            }
                        }
                        else
                        {
                            await ReaderWriter.UpdatePerson(user.username, user.email, user.friends, movieList);
                            await Navigation.PushAsync(new GridPage(user.email));
                            return;
                        }
                    }
                    finally
                    {
                        await PopupNavigation.Instance.PopAsync();
                    }
                }
                ChangeElements();
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", "There has been an unexpected error. Please try again!", "OK");
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
                likecounter++;
                if (MoviesQueue.Count == 0)
                {
                    await PopupNavigation.Instance.PushAsync(new BusyPopUp());

                    try
                    {
                        if (likecounter > 10)
                        {
                            bool decision = await DisplayAlert("Thats awkward", "You only liked less than 10 movies would you like to start over?", "Yes", "No");
                            if (decision == true)
                            {
                                if (restart == null)
                                {
                                    restart = new GenreAssignmentPage(user.email);
                                }
                                App.Current.MainPage = new NavigationPage(restart);
                            }
                            else
                            {
                                await ReaderWriter.UpdatePerson(user.username, user.email, user.friends, movieList);
                                await Navigation.PushAsync(new GridPage(user.email));
                                return;
                            }
                        }

                    }
                    finally
                    {
                        await PopupNavigation.Instance.PopAsync();
                    }
                }
                ChangeElements();
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", "There has been an unexpected error. Please try again!", "OK");
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
