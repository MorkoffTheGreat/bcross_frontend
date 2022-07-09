using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using bcross_frontend.Views;

namespace bcross_frontend
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
            Shell.SetTabBarIsVisible(this, false);
        }

        private async void loginBtn_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(AuthPage)}");
        }

        private async void regBtn_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}
