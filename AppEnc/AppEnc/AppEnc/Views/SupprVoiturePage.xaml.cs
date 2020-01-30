using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Net;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppEnc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SupprVoiturePage : ContentPage
    {
        public SupprVoiturePage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public async void Valider(object sender, EventArgs e)
        {
            if (Immatriculation != null)
            {
                WebRequest request = WebRequest.Create("http://127.0.0.1:1234/~berthel/?suppr=1&immatriculation=" + Immatriculation.Text);
                request.GetResponse();
                await Navigation.PopAsync();
            }
        }
    }
}