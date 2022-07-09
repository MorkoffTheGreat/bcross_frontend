using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using bcross_frontend.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using bcross_frontend.Services;

namespace bcross_frontend.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        IFirebaseAuthentication auth;
        RestService rest;
        public RegisterPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Shell.SetTabBarIsVisible(this, false);
            auth = DependencyService.Get<IFirebaseAuthentication>();
            rest = new RestService();
        }

        private async void regBtn_Clicked(object sender, EventArgs e)
        {
            if (usernameBox.Text is null || emailBox.Text is null || passBox.Text is null)
            {
                await DisplayAlert("Ошибка регистрации", "Заполните все обязательные поля", "Попробовать снова");
                return;
            }
            else if (!(phoneBox.Text is null) && (!long.TryParse(phoneBox.Text, out long n) || phoneBox.Text.Length != 11))
            {
                await DisplayAlert("Ошибка регистрации", "Некорректный номер телефона", "Попробовать снова");
                return;
            }
            else
                try
                {
                    var token = await auth.RegisterWithEmailAndPassword(emailBox.Text, passBox.Text);
                    if (token != string.Empty && token != "exist" && token != "cred")
                    {
                        var dbUser = new User()
                        {
                            Nickname = usernameBox.Text,
                            City = cityBox.Text,
                            Email = emailBox.Text,
                            Phone = phoneBox.Text,
                            Rating = 0
                        };

                        rest.AddUser(dbUser);
                        App.userId = await new RestService().GetUserIdByEmail(emailBox.Text);

                        await Shell.Current.Navigation.PopToRootAsync();
                    }
                    else if (token == "exist")
                    {
                        await DisplayAlert("Ошибка регистрации", "Пользователь с данным E-mail уже существует", "Попробовать снова");
                    }
                    else if (token == "cred")
                    {
                        await DisplayAlert("Ошибка регистрации", "Введён некорректный E-mail", "Попробовать снова");
                    }
                    else
                    {
                        await DisplayAlert("Ошибка регистрации", "Возможно, заполнены не все обязательные поля", "Попробовать снова");
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