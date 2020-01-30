using AppEnc.ViewsModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppEnc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaiementPageDetail : ContentPage
    {
        public PaiementPageDetail(Item item)
        {
            InitializeComponent();
            BindingContext = new PaiementViewModel(item, Navigation);
        }
    }
}