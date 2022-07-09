using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bcross_frontend.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bcross_frontend.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TakeBookPage : ContentPage
    {
        public TakeBookPage()
        {
            InitializeComponent();
        }

        private async void takeBookBtn_Clicked(object sender, EventArgs e)
        {
            RestService rest = new RestService();
            Guid uuid;
            if (!Guid.TryParse(guid.Text, out uuid))
            {
                await DisplayAlert("Некорректный номер книги", "Пожалуйста, введите корректный номер книги", "Попробовать снова");
                return;
            }
            if (await rest.GetBookByGuid(uuid) == 0)
            {
                await DisplayAlert("Книга с таким номером не найдена!", "Проверьте правильность написания номера", "Попробовать снова");
                return;
            }
            rest.TakeBook(Guid.Parse(guid.Text), App.userId, DateTime.UtcNow);
            await Navigation.PopToRootAsync();
        }
    }
}