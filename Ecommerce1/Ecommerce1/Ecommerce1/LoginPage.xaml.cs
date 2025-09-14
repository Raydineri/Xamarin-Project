using Ecommerce1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ecommerce1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly AuthService _authService;

        public LoginPage()
        {
            InitializeComponent();
            _authService = new AuthService();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string email = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ResultLabel.Text = "Please fill in both fields!";
                return;
            }

            if (email == "admin@gmail.com" && password == "admin")
            {
                await Navigation.PushAsync(new AdminTabbedPage());
                return;
            }

            var token = await _authService.LoginAsync(email, password);
            if (string.IsNullOrEmpty(token) || token == "Login failed!")
            {
                ResultLabel.Text = "Login failed!";
            }
            else
            {
                ResultLabel.Text = $"Login successful! Token: {token}";
                await Navigation.PushAsync(new MainPage());
            }
        }


        private async void OnNavigateToSignupClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }
    }
}