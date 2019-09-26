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

    public class Voiture
    {
        public Image Photo { get; set; }
        public int Imatriculation { get; set; }
    }
}