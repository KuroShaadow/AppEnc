using AppEnc.ViewsModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppEnc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VoituresPageDetail : ContentPage
    {
        readonly VoituresViewModel pageModel;
        public VoituresPageDetail()
        {
            InitializeComponent();
            BindingContext = pageModel = new VoituresViewModel(Navigation);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            pageModel.OnAppearing();
        }
        public async void AddItemCommand(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddVoiturePage());
        }
    }
}