using System;

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
        public Paiement Paiement { get; set; }
        public Formulaire Form { get; set; }
    }

    public class Voiture
    {
        public String Photo { get; set; }
        public int Imatriculation { get; set; }
        public string Lieu { get; set; }
    }

    public class DureePrix
    {
        public int Prix { get; set; }
        public int Duree { get; set; }
    }

    public class Paiement
    {
        public String MoyenPaiement { get; set; }
    }

    public class Formulaire
    {
        public String Prenom { get; set; }
        public String Nom { get; set; }
        public String Email { get; set; }
    }
}