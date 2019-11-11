using AppEnc.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppEnc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaiementPageDetail : ContentPage
    {
        PaiementViewModel pageModel;
        public PaiementPageDetail(Item item)
        {
            InitializeComponent();
            BindingContext = pageModel = new PaiementViewModel(item, Navigation);
        }
    }
}