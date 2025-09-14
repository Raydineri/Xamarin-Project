using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ecommerce1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            Application.Current.SavePropertiesAsync(); // Sauvegarde toutes les propriétés lorsque l'application se met en veille
        }

        protected override void OnResume()
        {
        }
    }
}
