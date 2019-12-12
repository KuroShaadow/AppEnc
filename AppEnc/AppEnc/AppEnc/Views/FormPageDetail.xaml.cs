using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppEnc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormPageDetail : ContentPage
    {
        Item Item;
        public FormPageDetail(Item item)
        {
            Item = item;
            InitializeComponent();
        }

        public void Entry_Completed(Object sender, EventArgs e)
        {
            if (Prenom.Text != null && Nom.Text != null && Mail.Text != null)
                Item.Form = new Formulaire { Prenom = Prenom.Text, Nom = Nom.Text, Email = Mail.Text};
        }

        public async void Valider(object sender, EventArgs e)
        {
            if (Item.Form != null)
            {
                MailMessage mail = new MailMessage("monpetit.petittest@gmail.com", Item.Form.Email, "Test", Item.ToString());
                SmtpClient smtpServer = new SmtpClient
                {
                    Port = 587,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("monpetit.petittest@gmail.com", "Test*1234")
                };
                try
                {
                    smtpServer.Send(mail);
                    WebRequest request = WebRequest.Create("http://192.168.0.26/?immatriculation=" + Item.Vehicule.Imatriculation + "&reservation=" + DateTime.Now.Minute + "&duree=" + Item.Prix.Duree);
                }
                catch (Exception) { }
                await Navigation.PopToRootAsync();
            }
        }
    }
}