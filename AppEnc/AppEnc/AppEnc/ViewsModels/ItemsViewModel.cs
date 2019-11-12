using AppEnc.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppEnc.ViewsModels
{
    public class VoituresViewModel : BindableObject
    {
        private INavigation Navigation;

        public VoituresViewModel(INavigation navigation)
        {
            Navigation = navigation;
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
                return new Command(async (data) =>
                {
                    await test();
                    //Navigation.PushAsync(new PrixPageDetail(new Item { Vehicule = data as Voiture }));
                });
            }
        }

        static readonly HttpClient client = new HttpClient();

        static async Task test()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://www.google.com/");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
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
