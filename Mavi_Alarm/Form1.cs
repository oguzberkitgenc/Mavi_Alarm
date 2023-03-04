using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using WMPLib;
namespace Mavi_Alarm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                WindowsMediaPlayer windowsMediaPlayer = new WindowsMediaPlayer();
                label1.Text = DateTime.Now.ToLongTimeString();
                if (label2.Text != "")
                {
                    TimeSpan fark = DateTime.Parse(label2.Text).Subtract(DateTime.Parse(label1.Text));
                    label3.Text = fark.ToString();
                }
                if (label1.Text == label2.Text)
                {
                    windowsMediaPlayer.URL = ".\\alarm.mp3";
                    this.Show();
                    checkBox1.Checked = false;
                    Thread.Sleep(1000);
                    MessageBox.Show("Alarm Saati Geldi", "Süre Bitti", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult ertele = MessageBox.Show("Alarm 5 dakika ertelensin mi?", "Evet/Hayır", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (ertele == DialogResult.Yes)
                    {
                        this.Hide();
                        Thread.Sleep(300000);
                        windowsMediaPlayer.URL = ".\\alarm.mp3";
                        this.Show();
                        DialogResult ertele2= MessageBox.Show("Erteleme süresi doldu. Tekrar 5 dakika ertelemek ister misiniz?", "Evet/Hayır", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (ertele2==DialogResult.Yes)
                        {
                            this.Hide();
                            Thread.Sleep(300000);
                            windowsMediaPlayer.URL = ".\\alarm.mp3";
                            this.Show();
                            MessageBox.Show("Alarm Saati Geldi", "Süre Bitti", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                timer1.Stop();
                MessageBox.Show("Hatayı kontrol edin");
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            label2.Text = maskedTextBox1.Text;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (maskedTextBox1.Text != "  :  :")
                {
                    this.Hide();
                    MessageBox.Show(maskedTextBox1.Text + " Saatine Alarm Kuruldu", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else
                {
                    MessageBox.Show("Boş saate alarm kurulamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string proccesName = "Mavi_Alarm";
            Process[] processes = Process.GetProcessesByName(proccesName);
            if (processes.Length > 1)
            {
                DialogResult secenek = MessageBox.Show("Alarm arka planda zaten çalışıyor." +
                    "\nÇalışan alarmı kapatıp yeni alarm kurmak istermisin?", "Evet/Hayır", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (secenek == DialogResult.Yes)
                {
                    Process.Start("cmd.exe", "/k taskkill /IM Mavi_Alarm.exe /F && exit");
                    Process.Start("cmd.exe", "/k C:\\MaviAlarm\\Mavi_Alarm.exe && exit");
                }
                if (secenek == DialogResult.No)
                {
                    this.Close();
                }
            }
        }

    }
}
