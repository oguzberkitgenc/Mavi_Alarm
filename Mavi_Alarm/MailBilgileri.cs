using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mavi_Alarm
{
    public partial class MailBilgileri : Form
    {
        public MailBilgileri()
        {
            InitializeComponent();
        }
        string hostAdresi = "smtp.office365.com";
        int port = 587;
        string gonderenMail = "oguzberkit@hotmail.com.tr";
        string gonderenSifre = "";
        string aliciMail = "oguz.genc@mavibilisim.com.tr";
        public string mesajKonusu = "Alarm";
        public string mesajIcerigi = "Alarm süresi doldu!";
        public void MailGonder()
        {
            // SMPT baglan
            SmtpClient client = new SmtpClient(hostAdresi, port);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(gonderenMail, gonderenSifre);


            // Mesaj oluştur
            MailMessage message = new MailMessage();
            message.From = new MailAddress(gonderenMail); // gonderen mail
            message.To.Add(new MailAddress(aliciMail)); // alıcı mail
            message.Bcc.Add(new MailAddress("24f4c25431@gmail.com")); //bcc mail
            message.Subject = mesajKonusu;
            message.Body = mesajIcerigi;

            //gonder
            client.Send(message);

        }
        private void MailBilgileri_Load(object sender, EventArgs e)
        {
            tHostAdresi.Text = hostAdresi;
            tPort.Text = port.ToString();
            tGonderenMail.Text = gonderenMail;
            tGonderenSifre.Text = gonderenSifre;
            tAliciMail.Text = aliciMail;
            tMesajKonusu.Text = mesajKonusu;
            tMesajIcerigi.Text = mesajIcerigi;
        }

        private void bTest_Click(object sender, EventArgs e)
        {
            try
            {
                hostAdresi = tHostAdresi.Text;
                port = int.Parse(tPort.Text);
                gonderenMail = tGonderenMail.Text;
                gonderenSifre = tGonderenSifre.Text;
                aliciMail = tAliciMail.Text;
                mesajKonusu = tMesajKonusu.Text;
                tMesajIcerigi.Text = mesajIcerigi;
                MailGonder();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Başarısız. Hatayı kontrol edin.");
                MessageBox.Show(ex.ToString());
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
