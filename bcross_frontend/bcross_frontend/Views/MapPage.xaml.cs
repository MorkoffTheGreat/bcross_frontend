using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

using bcross_frontend.Services;
using bcross_frontend.Models;

namespace bcross_frontend.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        RestService rest;
        List<Bookrecord> records;
        public MapPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Initialize();
        }

        public void Initialize()
        {
            activityIndicator.IsRunning = true;
            map.IsShowingUser = true;
            rest = new RestService();
            records = rest.GetAllPutBooks();
            map.Pins.Clear();
            PutMarkers();
            activityIndicator.IsRunning = false;
            dimRect.IsVisible = false;
            MoveToUser();
        }

        public async void MoveToUser()
        {
            var pos = await Geolocation.GetLocationAsync();
            MapSpan span = new MapSpan(new Position(pos.Latitude, pos.Longitude), 1, 1);
            map.MoveToRegion(span);
        }

        public void PutMarkers()
        {
            foreach (var record in records)
            {
                var coords = record.Location;
                coords = coords.Replace(',', '.');
                string[] tokens = coords.Split();
                double x, y;
                x = Double.Parse(tokens[0], CultureInfo.InvariantCulture);
                y = Double.Parse(tokens[1], CultureInfo.InvariantCulture);


                Pin pin = new Pin();
                pin.Position = new Position(x, y);
                Book book = rest.GetBookById(record.Book);
                pin.MarkerClicked += async (sender, e) =>
                {
                    await DisplayAlert($"{book.Author} - {book.Name} ({book.Year})", record.Commentary, "Понял!");
                };
                pin.Label = $"{book.Author} - {book.Name}";
                map.Pins.Add(pin);
            }
        }
    }
}