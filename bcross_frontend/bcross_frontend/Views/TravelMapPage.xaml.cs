using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;

namespace bcross_frontend.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TravelMapPage : ContentPage
    {
        List<string> locations;
        public TravelMapPage(List<string> locations, Guid guid)
        {
            Shell.SetTabBarIsVisible(this, false);
            this.locations = locations;
            InitializeComponent();
            guidLabel.Text = guid.ToString();
            PutMarkers();
        }

        public void PutMarkers()
        {
            foreach (var loc in locations)
            {
                var coords = loc.Replace(',', '.');
                string[] tokens = coords.Split();
                Pin pin = new Pin();
                pin.Position = new Position(double.Parse(tokens[0], CultureInfo.InvariantCulture), double.Parse(tokens[1], CultureInfo.InvariantCulture));
                pin.Label = "There will be time";
                map.Pins.Add(pin);
            }
        }
    }
}