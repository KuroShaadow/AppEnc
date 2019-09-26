using AppEnc.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace AppEnc.ViewsModels
{
    public class VoituresViewModel : BindableObject
    {
        private VoituresPageDetail mainPage;

        public VoituresViewModel(VoituresPageDetail mainPage)
        {
            this.mainPage = mainPage;
            AddItems();
        }

        private void AddItems()
        {
            for (int i = 0; i < 20; i++)
                Items.Add(new Voiture() { Imatriculation = i * 1000, Photo = new Image() }) ;
        }

        private ObservableCollection<Voiture> _items = new ObservableCollection<Voiture>();
        public ObservableCollection<Voiture> Items
        {
            get
            {
                return _items;
            }
            set
            {
                if (_items != value)
                {
                    _items = value;
                    OnPropertyChanged(nameof(Items));
                }
            }
        }

        public Command ItemTappedCommand
        {
            get
            {
                return new Command((data) =>
                {
                    mainPage.DisplayAlert("FlowListView", data + "", "Ok");
                });
            }
        }
    }
}
