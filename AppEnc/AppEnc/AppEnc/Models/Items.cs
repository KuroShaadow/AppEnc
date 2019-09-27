using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppEnc.Views
{

    public class MenuItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class Item
    {
        public Voiture Vehicule { get; set; }
        public DureePrix Prix { get; set; }
    }

    public class Voiture
    {
        public Image Photo { get; set; }
        public int Imatriculation { get; set; }
    }

    public class DureePrix
    {
        public int Prix { get; set; }
        public int Duree { get; set; }
    }
}