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
    public partial class PrixPageDetail : ContentPage
    {
        PrixViewModel pageModel;
        public PrixPageDetail(Item item)
        {
            InitializeComponent();
            BindingContext = pageModel = new PrixViewModel(item, Navigation);
        }
    }
}