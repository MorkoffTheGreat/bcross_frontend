using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using bcross_frontend.Models;
using bcross_frontend.Services;

namespace bcross_frontend.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        RestService rest;
        IFirebaseAuthentication auth;
        public ProfilePage()
        {
            InitializeComponent();
            rest = new RestService();
            auth = DependencyService.Get<IFirebaseAuthentication>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                User user = rest.GetUserById(App.userId);
                profileNameLabel.Text = $"{user.Nickname} ({user.Rating})";
            }
            catch
            {
                DisplayAlert("Ошибка", "Нет соединения с сервером", "OK");
            }
        }

        private async void putBtn_Clicked(object sender, EventArgs e)
        {
            var books = await rest.GetSentBooks(App.userId);
            await Navigation.PushAsync(new ProfileBooksPage(books));
        }

        private async void takenBtn_Clicked(object sender, EventArgs e)
        {
            var books = await rest.GetReceivedBooks(App.userId);
            await Navigation.PushAsync(new ProfileBooksPage(books));
        }

        private async void logoffBtn_Clicked(object sender, EventArgs e)
        {
            if (auth.SignOut())
            {
                await Navigation.PushModalAsync(new LoginPage());
            }
        }

        private async void aboutBtn_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("О приложении", "Разработчик:\nMorkoff\ngithub.com/MorkoffTheGreat\n\nМоральная поддержка:\ntangenx\ngithub.com/tangenx\n\nИконки by Freepik", "OK");
        }
    }
}