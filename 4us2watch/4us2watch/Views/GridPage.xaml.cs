using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace _4us2watch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GridPage : ContentPage
    {
        ProfilePage profile = null;
        GridPage grid = null;
        public GridPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = this;
        }
        public ICommand HelpCommand => new Command(onHelpClick);
        public ICommand LogOutCommand => new Command(onLogOutClick);
        public ICommand HomeCommand => new Command(onHomeClick);
        public ICommand FriendsCommand => new Command(onFriendsClick);
        public ICommand ProfileCommand => new Command(onProfileClick);
        public ICommand MoviesCommand => new Command(onMoviesClick);
        public ICommand TVSeriesCommand => new Command(onTVSeriesClick);
        public ICommand DocumentariesCommand => new Command(onDocumentariesClick);
        public ICommand AnimeCommand => new Command(onAnimeClick);

        public async void onHelpClick()
        {
            await DisplayAlert("Dela", "Dela", "OK");
        }
        public async void onLogOutClick()
        {
            await DisplayAlert("Dela", "Dela", "OK");
        }
        public void onHomeClick()
        {
            if (grid == null)
            {
                grid = new GridPage();
            }
            App.Current.MainPage = new NavigationPage(grid);
        }
        public async void onFriendsClick()
        {
            await DisplayAlert("Dela", "Dela", "OK");
        }
        public void onProfileClick()
        {
            if (profile == null)
            {
                profile = new ProfilePage();
            }
            App.Current.MainPage = new NavigationPage(profile);
        }
        public async void onMoviesClick()
        {
            await DisplayAlert("Dela", "Dela", "OK");
        }
        public async void onTVSeriesClick()
        {
            await DisplayAlert("Dela", "Dela", "OK");
        }
        public async void onDocumentariesClick()
        {
            await DisplayAlert("Dela", "Dela", "OK");
        }
        public async void onAnimeClick()
        {
            await DisplayAlert("Dela", "Dela", "OK");
        }
    }
}