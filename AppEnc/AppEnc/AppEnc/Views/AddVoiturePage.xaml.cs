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
                RotateImage = false
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
                await client.PostAsync("http://127.0.0.1:1234/~berthel/?add=1&immatriculation=" + Immatriculation.Text, content);

                await Navigation.PopAsync();
            }
            else
            {
                WebRequest request = WebRequest.Create("http://127.0.0.1:1234/~berthel/?add=1&immatriculation=" + Immatriculation.Text);
                request.GetResponse();

                await Navigation.PopAsync();
            }
        }
    }
}