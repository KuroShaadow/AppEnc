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
            InitializeComponent();
        }

        public void Entry_Completed(Object sender, EventArgs e)
        {
            if (Prenom.Text != null && Nom.Text != null && Mail.Text != null)
                Item.Form = new Formulaire { Prenom = Prenom.Text, Nom = Nom.Text, Email = Mail.Text };
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
                WebRequest request = WebRequest.Create("http://127.0.0.1:1234/~berthel/?immatriculation=" + Item.Vehicule.Imatriculation + "&reservation=" + (DateTime.Now.Hour * 60 + DateTime.Now.Minute) + "&duree=" + Item.Prix.Duree);
                request.GetResponse();
                try
                {
                    smtpServer.Send(mail);
                }
                catch (Exception) { }
                await Navigation.PopToRootAsync();
            }
        }
    }
}