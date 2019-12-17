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
    public partial class AddVoiturePage : ContentPage
    {
        MediaFile PhotoImage;
        public AddVoiturePage()
        {
            InitializeComponent();
            PhotoImage = null;
            BindingContext = this;
        }

        public async void AddPhoto(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Name = "image.jpg",
                SaveToAlbum = false,
                MaxWidthHeight = 150
            });
            if (file != null)
            {
                PhotoImage = file;
            }
        }

        public async void Valider(object sender, EventArgs e)
        {

            if (PhotoImage != null)
            {
                var client = new HttpClient();
                var content = new MultipartFormDataContent
            {
                { new StreamContent(PhotoImage.GetStream()), "\"image\"", $"\"{ PhotoImage.Path}\"" }
            };
                await client.PostAsync("http://192.168.0.24/?add=1&immatriculation=" + Immatriculation.Text, content);
            }
            else
            {
                WebRequest request = WebRequest.Create("http://192.168.0.24/?add=1&immatriculation=" + Immatriculation.Text);
                request.GetResponse();
            }

            await Navigation.PopAsync();
        }
    }
}