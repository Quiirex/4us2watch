using _4us2watch.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace _4us2watch
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            // Login in to be implemented
            await Navigation.PushAsync(new LoginPage());
        }

        private async void Register_Clicked(object sender, EventArgs e)
        {
            // Register to be implemented (add async)
            await Navigation.PushAsync(new RegistrationPage());
        }
    }
}
