﻿using AppEnc.ViewsModels;
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
    public partial class VoituresPageDetail : ContentPage
    {
        VoituresViewModel pageModel;
        public VoituresPageDetail(MainPage main)
        {
            InitializeComponent();
            pageModel = new VoituresViewModel(main);
            BindingContext = pageModel;
        }
    }
}