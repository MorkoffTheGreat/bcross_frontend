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
    public partial class ProfileBooksPage : ContentPage
    {
        RestService rest;
        public ProfileBooksPage(List<Book> books)
        {
            Shell.SetTabBarIsVisible(this, false);
            rest = new RestService();
            ListView listView = new ListView();
            InitializeComponent();
            listView.ItemsSource = books;
            listView.SeparatorVisibility = SeparatorVisibility.None;
            listView.ItemTemplate = new DataTemplate(typeof(TextCell));
            listView.ItemTemplate.SetValue(TextCell.TextColorProperty, Color.Black);
            listView.ItemTemplate.SetBinding(TextCell.TextProperty, ".");
            listView.ItemSelected += async (sender, e) =>
            {
                Guid bookid = ((Book)e.SelectedItem).Uuid;
                var locations = await rest.GetBookTravel(bookid);
                await Navigation.PushAsync(new TravelMapPage(locations, bookid));
            };
            layout.Children.Add(listView);
        }
    }
}