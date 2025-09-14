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
    public partial class SignUpPage : ContentPage
    {
        private readonly AuthService _authService = new AuthService();
        public SignUpPage()
        {
            InitializeComponent();
            _authService = new AuthService(); // Initialize AuthService

        }


        private async void OnSignupClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;
            string email = emailEntry.Text;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ResultLabel.Text = "Please fill in both fields!";
                return;
            }

            var result = await _authService.SignupAsync(email, password, username);
            ResultLabel.Text = result;
            await Navigation.PopAsync();  // Navigating back to LoginPage

        }

        private async void OnNavigateToLoginClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();  // Navigating back to LoginPage
        }
    }
}