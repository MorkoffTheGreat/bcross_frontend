using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using bcross_frontend.Services;

namespace bcross_frontend
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthPage : ContentPage
    {
        RestService rest;
        IFirebaseAuthentication auth;
        public AuthPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Shell.SetTabBarIsVisible(this, false);
            auth = DependencyService.Get<IFirebaseAuthentication>();
        }

        private async void lessgoBtn_Clicked(object ssender, EventArgs e)
        {
            try
            {
                string token = await auth.LoginWithEmailAndPassword(loginBox.Text, passBox.Text);
                if (token != string.Empty)
                {
                    rest = new RestService();
                    App.userId = await rest.GetUserIdByEmail(auth.GetEmail());
                    await Shell.Current.Navigation.PopToRootAsync();
                }
                else
                {
                    await DisplayAlert("Ошибка входа", "Неправильный логин или пароль", "Попробовать снова");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert(ex.Message, $"Возможно, нет подключения к интернету, попробуйте позже\n\nОтладочная информация:\n\n{ex.StackTrace}", "Выход из приложения");
                var close = DependencyService.Get<ICloseApplication>();
                close.CloseApp();
            }
        }
    }
}