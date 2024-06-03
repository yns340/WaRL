using System.Diagnostics;
using static WinFormsApp1.FormGirisEkrani;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private string _username;
        private int kullanýcýID;

        public Form1(string username, int kullanýcýID)
        {
            InitializeComponent();
            this._username = username;
            this.kullanýcýID = kullanýcýID;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateUserLinkLabel();
            CenterLabel();
            ResizePanels();
            ResizePictureBoxes();
            ResizeButtons();
        }

        private void UpdateUserLinkLabel()
        {
            linkLabel1.Text = _username;
            label2.Text = "ID:" + Convert.ToString(kullanýcýID);
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

        private void ResizePictureBoxes()
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

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(KullanýcýGiriþi.KullanýcýAdý, KullanýcýGiriþi.KullanýcýID);
            form2.ClientSize = this.ClientSize;

            if (this.WindowState == FormWindowState.Maximized)
            {
                form2.WindowState = FormWindowState.Maximized;
            }

            this.Hide();
            form2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(_username, kullanýcýID);
            form3.ClientSize = this.ClientSize;

            if (this.WindowState == FormWindowState.Maximized)
            {
                form3.WindowState = FormWindowState.Maximized;
            }

            this.Hide();
            form3.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
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
