using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace bcross_frontend.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PutMapPage : ContentPage
    {
        public PutMapPage()
        {
            InitializeComponent();
            MoveToUser();
        }

        public async void MoveToUser()
        {
            var pos = await Geolocation.GetLocationAsync();
            MapSpan span = new MapSpan(new Position(pos.Latitude, pos.Longitude), 1, 1);
            map.MoveToRegion(span);
        }

        private async void map_MapClicked(object sender, Xamarin.Forms.Maps.MapClickedEventArgs e)
        {
            string loc = $"{e.Position.Latitude} {e.Position.Longitude}";
            ((PutBookPage)Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]).UpdateLocEntry(loc);
            await Navigation.PopAsync();
        }
    }
}