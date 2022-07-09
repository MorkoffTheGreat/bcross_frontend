using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bcross_frontend
{
    public partial class App : Application
    {
        public static int userId = 0;
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
