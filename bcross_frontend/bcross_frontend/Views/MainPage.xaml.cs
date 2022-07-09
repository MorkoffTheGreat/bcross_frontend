using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

using bcross_frontend.Views;
using bcross_frontend.Services;

namespace bcross_frontend
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : Shell
    {
        IFirebaseAuthentication auth;
        RestService rest;
        ICloseApplication close;
        public MainPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<IFirebaseAuthentication>();
            close = DependencyService.Get<ICloseApplication>();
            Init();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Init();
        }

        public async void Init()
        {
            try
            {
                Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
                Routing.RegisterRoute(nameof(AuthPage), typeof(AuthPage));
                Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
                var signedIn = auth.IsSignIn();
                if (!signedIn)
                    await Navigation.PushModalAsync(new LoginPage());
                else
                {
                    rest = new RestService();
                    App.userId = await rest.GetUserIdByEmail(auth.GetEmail());
                }
            }
            catch (Exception e)
            {
                await DisplayAlert(e.Message, $"Возможно, нет подключения к интернету, попробуйте позже\n\nОтладочная информация:\n\n{e.StackTrace}", "Выход из приложения");
                close.CloseApp();
            }
        }
    }
}