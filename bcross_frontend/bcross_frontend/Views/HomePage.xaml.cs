using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bcross_frontend.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        IFirebaseAuthentication auth;
        public HomePage()
        {
            InitializeComponent();
            auth = DependencyService.Get<IFirebaseAuthentication>();
        }

        private async void addBookBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PutBookPage());
        }

        private async void takeBookBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TakeBookPage());
        }

        private async void howToBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HowToPage());
        }
    }
}