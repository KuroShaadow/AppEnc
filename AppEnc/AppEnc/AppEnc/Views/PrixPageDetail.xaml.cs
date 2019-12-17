using AppEnc.ViewsModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppEnc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrixPageDetail : ContentPage
    {
        public PrixPageDetail(Item item)
        {
            InitializeComponent();
            BindingContext = new PrixViewModel(item, Navigation);
        }
    }
}