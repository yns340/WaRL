using System.Diagnostics;
using static WinFormsApp1.FormGirisEkrani;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private string _username;
        private int kullan�c�ID;

        public Form1(string username, int kullan�c�ID) //giri� ekran�ndan al�nan kullan�c� id si ve kullan�c� ad�n� parametre olarak ald�k
        {
            InitializeComponent();
            this._username = username;
            this.kullan�c�ID = kullan�c�ID;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateUserLinkLabel();
            CenterLabel();
            ResizePanels();
            ResizePictureBoxes();
            ResizeButtons();
            linkLabel1.Left = pictureBox3.Left - linkLabel1.Width;
            label2.Left = linkLabel1.Left;
        }

        private void UpdateUserLinkLabel()
        {
            linkLabel1.Text = _username;
            label2.Text = "ID:" + Convert.ToString(kullan�c�ID); //parametre olarak ald���m�z kullan�c� id ve kullan�c� ad�n� etiket olarak g�sterdik
        }

        private void CenterLabel()
        {
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
        }

        private void ResizePanels()
        {
            int panelWidth = this.ClientSize.Width;
            panel1.Width = panelWidth / 2;
            panel2.Width = panelWidth / 2;

            int panelHeight = this.ClientSize.Height;
            panel1.Height = panelHeight;
            panel2.Height = panelHeight;

            panel1.Left = 0;
            panel2.Left = panel1.Width;
        }

        private void ResizePictureBoxes() //film/dizi ve kitap butonlar�n�n �st�ne koyulan resimlerin boyutlar�
        {
            if (pictureBox1 != null)
            {
                pictureBox1.ClientSize = new Size(this.ClientSize.Width / 2, (62 * (this.ClientSize.Width / 2)) / 92);
            }

            if (pictureBox2 != null)
            {
                pictureBox2.ClientSize = new Size(this.ClientSize.Width / 2, (62 * (this.ClientSize.Width / 2)) / 92);
            }
        }

        private void ResizeButtons()
        {
            button1.ClientSize = new Size(this.ClientSize.Width / 2, panel1.Height - pictureBox1.Height);
            button2.ClientSize = new Size(this.ClientSize.Width / 2, panel2.Height - pictureBox1.Height);
            button1.Top = pictureBox1.Bottom;
            button2.Top = pictureBox2.Bottom;
        }

        private void button1_Click(object sender, EventArgs e) // film/dizi sekmesine gitmeyi sa�layan butonun fonksiyonu
        {
            Form2 form2 = new Form2(Kullan�c�Giri�i.Kullan�c�Ad�, Kullan�c�Giri�i.Kullan�c�ID);
            form2.ClientSize = this.ClientSize;

            if (this.WindowState == FormWindowState.Maximized)
            {
                form2.WindowState = FormWindowState.Maximized;
            }

            this.Hide();
            form2.Show();
        }

        private void button2_Click(object sender, EventArgs e)  //kitap sekmesine gitmeyi sa�layan butonun fonksiyonu
        {
            Form3 form3 = new Form3(_username, kullan�c�ID);
            form3.ClientSize = this.ClientSize;

            if (this.WindowState == FormWindowState.Maximized)
            {
                form3.WindowState = FormWindowState.Maximized;
            }

            this.Hide();
            form3.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) //form 2 veya form 3 e ge�i�i sa�layan butonlara bas�ld�ktan sonra form1 in arkaplanda kapanmas�n� sa�layan fonksiyon
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            label2.Left = linkLabel1.Left;
            CenterLabel();
            ResizePanels();
            ResizePictureBoxes();
            ResizeButtons();
        }
    }
}
