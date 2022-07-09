using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using bcross_frontend.Services;
using bcross_frontend.Models;

namespace bcross_frontend.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatsPage : ContentPage
    {
        RestService rest;
        ListView listView;
        public StatsPage()
        {
            InitializeComponent();
            rest = new RestService();
            listView = new ListView();
            listView.ItemsSource = rest.GetScoreboard();
            // listView.SeparatorVisibility = SeparatorVisibility.None;
            listView.ItemTemplate = new DataTemplate(typeof(TextCell));
            listView.ItemTemplate.SetValue(TextCell.TextColorProperty, Color.Black);
            listView.ItemTemplate.SetBinding(TextCell.TextProperty, ".");
            layout.Children.Add(listView);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = null;
            listView.ItemsSource = rest.GetScoreboard();
        }
    }
}