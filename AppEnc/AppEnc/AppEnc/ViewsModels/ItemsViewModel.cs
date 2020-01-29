using AppEnc.Views;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using Xamarin.Forms;

namespace AppEnc.ViewsModels
{
    public class VoituresViewModel : BindableObject
    {
        private readonly INavigation Navigation;

        public VoituresViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }

        private async void AddItems()
        {
            WebRequest request = WebRequest.Create("http://192.168.0.24/");
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
                    int reservation = (int)voiture.GetValue("reservation");
                    int immatriculation = (int)voiture.GetValue("immatriculation");
                    int duree = (int)voiture.GetValue("duree");
                    string photo = "http://192.168.0.24/images/" + (string)voiture.GetValue("photo");
                    string lieu = (string)voiture.GetValue("lieu");

                    if (reservation != -1 && (reservation + duree) <= DateTime.Now.Hour * 60 + DateTime.Now.Minute)
                    {
                        request = WebRequest.Create("http://192.168.0.24/?retour=1&immatriculation=" + immatriculation);
                        request.GetResponse();
                    }
                    if (reservation == -1)
                    {
                        Items.Add(new Voiture
                        {
                            Imatriculation = immatriculation,
                            Photo = photo,
                            Lieu = lieu
                        });
                    }
                }
            }
        }

        public void OnAppearing()
        {
            Items.Clear();
            AddItems();
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
                return new Command(() =>
                {
                    IsBusy = true;
                    OnAppearing();
                    IsBusy = false;
                });
            }
        }

        public Command ItemTappedCommand
        {
            get
            {
                return new Command(async (data) =>
                {
                    await Navigation.PushAsync(new PrixPageDetail(new Item { Vehicule = (Voiture)data }));
                });
            }
        }
    }

    public class PrixViewModel : BindableObject
    {
        private readonly INavigation Navigation;
        private readonly Item Item;

        public PrixViewModel(Item item, INavigation navigation)
        {
            Item = item;
            Navigation = navigation;
            AddItems();
        }

        private void AddItems()
        {
            Items.Add(new DureePrix() { Duree = 1, Prix = 5 });
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
                    Item.Prix = (DureePrix)data;
                    await Navigation.PushAsync(new PaiementPageDetail(Item));
                });
            }
        }
    }

    public class PaiementViewModel : BindableObject
    {
        private readonly INavigation Navigation;
        private readonly Item Item;

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
                    Item.Paiement = (Paiement)data;
                    await Navigation.PushAsync(new FormPageDetail(Item));
                });
            }
        }
    }
}
