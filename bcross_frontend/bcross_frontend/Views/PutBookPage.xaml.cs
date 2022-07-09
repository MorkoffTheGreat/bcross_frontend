using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bcross_frontend.Models;
using bcross_frontend.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bcross_frontend.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PutBookPage : ContentPage
    {
        RestService rest;
        int bookid;
        public string location;
        public PutBookPage()
        {
            InitializeComponent();
            rest = new RestService();
            InitPage();
        }

        public void InitPage()
        {

        }

        private void ExistingBook_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            GuidField.IsEnabled = ExistingBook.IsChecked;
            AuthorField.IsEnabled = NewBook.IsChecked;
            NameField.IsEnabled = NewBook.IsChecked;
            YearField.IsEnabled = NewBook.IsChecked;
        }

        private async void PutBtn_Clicked(object sender, EventArgs e)
        {
            if (ExistingBook.IsChecked)
            {
                if (GuidField.Text is null || !Guid.TryParse(GuidField.Text, out Guid guid))
                {
                    await DisplayAlert("Некорректный номер книги", "Пожалуйста, введите корректный номер книги", "Попробовать снова");
                    return;
                }
                bookid = await rest.GetBookByGuid(Guid.Parse(GuidField.Text));
            }
            else
            {
                if (NameField.Text is null || AuthorField.Text is null || YearField.Text is null)
                {
                    await DisplayAlert("Пожалуйста, заполните все поля", "Все поля обязательны для заполнения", "Попробовать снова");
                    return;
                }
                else if (!int.TryParse(YearField.Text, out int num) || YearField.Text.Length != 4)
                {
                    await DisplayAlert("Некорректный год выпуска книги", "Пожалуйста, введите корректный год выпуска книги", "Попробовать снова");
                    return;
                }
                bookid = await rest.GetNextBookId();
                rest.AddBook(NameField.Text, AuthorField.Text, YearField.Text);
            }

            if (bookid != 0)
            {
                bookLayout.IsVisible = false;
                putLayout.IsVisible = true;
            }
            else
            {
                await DisplayAlert("Книга с таким номером не найдена!", "Проверьте правильность написания номера", "Попробовать снова");
            }

        }

        private async void putRecordBtn_Clicked(object sender, EventArgs e)
        {
            if (locEntry.Text is null || commentEntry.Text is null)
            {
                await DisplayAlert("Пожалуйста, заполните все поля", "Все поля обязательны для заполнения", "Попробовать снова");
                return;
            }
            var guid = await rest.PutBook(bookid, App.userId, locEntry.Text, DateTime.UtcNow, commentEntry.Text);
            await DisplayAlert("Книга размещена!", $"Пожалуйста, запишите данный номер:\n{guid}", "Записал!");
            await Navigation.PopToRootAsync();
        }

        private async void locButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PutMapPage());
        }

        public void UpdateLocEntry(string str)
        {
            locEntry.Text = str;
        }
    }
}