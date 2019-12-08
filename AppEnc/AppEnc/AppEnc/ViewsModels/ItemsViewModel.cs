using AppEnc.Views;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppEnc.ViewsModels
{
    public class VoituresViewModel : BindableObject
    {
        private INavigation Navigation;

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        public Command LoadItemsCommand
        {
            get
            {
                return new Command( () => 
                {
                    IsBusy = true;
                    Items.Clear();
                    AddItems();
                    IsBusy = false;
                });
            }
        }

        public VoituresViewModel(INavigation navigation)
        {
            Navigation = navigation;
            AddItems();
        }

        private async void AddItems()
        {
            WebRequest request = WebRequest.Create("http://192.168.0.26/");
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "GET";
            request.ContentType = "application/json";
            WebResponse response = request.GetResponse();
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                JObject json = JObject.Parse(await reader.ReadToEndAsync());
                foreach (JObject voiture in (JArray)json.GetValue("voiture"))
                {
                    Items.Add(new Voiture
                    {
                        Imatriculation = (int)voiture.GetValue("immatriculation"),
                        Photo = (string)voiture.GetValue("photo"),
                        Lieu = (string)voiture.GetValue("lieu")
                    });
                }
            }
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
                return new Command(async (data) =>
                {
                    await Navigation.PushAsync(new PrixPageDetail(new Item { Vehicule = (Voiture)data }));
                    //Navigation.PushAsync(new PrixPageDetail(new Item { Vehicule = data as Voiture }));
                });
            }
        }
    }

    public class PrixViewModel : BindableObject
    {
        private INavigation Navigation;
        private Item Item;

        public PrixViewModel(Item item, INavigation navigation)
        {
            Item = item;
            Navigation = navigation;
            AddItems();
        }

        private void AddItems()
        {
            Items.Add(new DureePrix() { Duree = 7, Prix = 5 });
            Items.Add(new DureePrix() { Duree = 15, Prix = 8 });
            Items.Add(new DureePrix() { Duree = 20, Prix = 10 });
            Items.Add(new DureePrix() { Duree = 30, Prix = 15 });
            Items.Add(new DureePrix() { Duree = 45, Prix = 20 });
            Items.Add(new DureePrix() { Duree = 60, Prix = 25 });
        }

        private ObservableCollection<DureePrix> _items = new ObservableCollection<DureePrix>();
        public ObservableCollection<DureePrix> Items
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
                return new Command(async (data) =>
                {
                    await Navigation.PushAsync(new PaiementPageDetail(Item));
                });
            }
        }
    }

    public class PaiementViewModel : BindableObject
    {
        private INavigation Navigation;
        private Item Item;

        public PaiementViewModel(Item item, INavigation navigation)
        {
            Item = item;
            Navigation = navigation;
            AddItems();
        }

        private void AddItems()
        {
            Items.Add(new Paiement { MoyenPaiement = "Carte Bancaire" });
            Items.Add(new Paiement { MoyenPaiement = "Espèce" });
            Items.Add(new Paiement { MoyenPaiement = "Cheque" });
        }

        private ObservableCollection<Paiement> _items = new ObservableCollection<Paiement>();
        public ObservableCollection<Paiement> Items
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
                return new Command(async (data) =>
                {
                    await Navigation.PushAsync(new FormPageDetail(Item));
                });
            }
        }
    }
}
