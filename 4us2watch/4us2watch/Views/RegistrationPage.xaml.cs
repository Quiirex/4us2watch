using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace _4us2watch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        IAuth auth;
        public RegistrationPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            auth = DependencyService.Get<IAuth>();
        }

        private async void BtnRegister_Clicked(object sender, EventArgs e)
        {
            if(password.Text != retypedPassword.Text)
            {
                await DisplayAlert("Error", "Passwords do not match", "OK");
                return;
            }

            bool created = await auth.SignUpWithEmailPassword(Email.Text, password.Text);
           // Console.WriteLine(created);
            if (created)
            {
                await DisplayAlert("Success", "Registration successful", "OK");
                await Navigation.PushAsync(new LoginPage()); //vrni na login, ko je registracija uspešna
                //await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Failed", "Registration unsuccessful, check the credentials", "OK");
            }

        }
    }
}