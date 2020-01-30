using System;
using System.Net;
using System.Net.Mail;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppEnc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormPageDetail : ContentPage
    {
        readonly Item Item;
        public FormPageDetail(Item item)
        {
            Item = item;
            Item.Form = new Formulaire();
            InitializeComponent();
        }

        public void Entry_Completed(Object sender, EventArgs e)
        {
            if (Prenom.Text != null)
                Item.Form.Prenom = Prenom.Text;
            if (Nom.Text != null)
                Item.Form.Nom = Nom.Text;
            if (Mail.Text != null)
                Item.Form.Email = Mail.Text;
        }

        public async void Valider(object sender, EventArgs e)
        {
            if (Item.Form.Prenom != null && Item.Form.Nom != null && Item.Form.Email != null)
            {
                string contentMail = "Véhicule : " + Item.Vehicule.Imatriculation +
                                    "\nDurée : " + Item.Prix.Duree +
                                    "\nPrix : " + Item.Prix.Prix +
                                    "\nMoyen de Paiement : " + Item.Paiement.MoyenPaiement;
                MailMessage mail = new MailMessage("monpetit.petittest@gmail.com", Item.Form.Email, "Reçu reservation véhicule", contentMail);
                SmtpClient smtpServer = new SmtpClient
                {
                    Port = 587,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("monpetit.petittest@gmail.com", "Test*1234")
                };
                WebRequest request = WebRequest.Create("http://127.0.0.1:1234/~berthel/?immatriculation=" + Item.Vehicule.Imatriculation + "&reservation=" + (DateTime.Now.Hour * 60 + DateTime.Now.Minute) + "&duree=" + Item.Prix.Duree);
                request.GetResponse();
                try
                {
                    smtpServer.Send(mail);
                }
                catch (Exception) { }
                await Navigation.PopToRootAsync();
            }
            else
            {
                string incomplet = "";
                if (Item.Form.Prenom == null)
                    incomplet += "Prenom";
                if (Item.Form.Nom == null)
                    incomplet += (Item.Form.Prenom == null ? ", " : "") + "Nom";
                if (Item.Form.Email == null)
                    incomplet += (Item.Form.Prenom == null || Item.Form.Nom == null ? ", " : "") + "Email";

                await DisplayAlert("Formulaire incomplet", incomplet, "Retour");
            }
        }
    }
}