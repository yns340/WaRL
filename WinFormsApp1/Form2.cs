using System;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
        private string username;
        private int kullanıcıID;

        // Form2'nin parametre alan yapılandırıcısı
        public Form2(string username, int kullanıcıID)
        {
            InitializeComponent();
            this.username = username; // Gelen kullanıcı adını sakla
            this.kullanıcıID = kullanıcıID;
            this.FormClosing += new FormClosingEventHandler(Form2_Closing);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            ResizePictureBox();

            button1.Height = label1.Height;
            button1.Width = label1.Height;
            Form2_Resize(this, EventArgs.Empty);
        }

        private void Form2_Resize(object sender, EventArgs e)
        {
            ResizePictureBox();
            button1.Height = label1.Height;
            button1.Width = label1.Height;

            int button2middle = this.ClientSize.Width / 4;
            int button3middle = (3 * (this.ClientSize.Width)) / 4;
            button2.Height = 351 * ClientSize.Height / 1105;
            button3.Height = button2.Height;
            button2.Width = button2.Height;
            button3.Width = button3.Height;

            button2.Top = ClientSize.Height / 2 - button2.Height / 2;
            button3.Top = ClientSize.Height / 2 - button3.Height / 2;

            button2.Left = button2middle - (button2.Width / 2);
            button3.Left = button3middle - (button3.Width / 2);

            label1.Left = (ClientSize.Width - label1.Width) / 2;

            label2.Width = button2.Width;
            label2.Top = button2.Bottom;
            label2.Left = button2middle - (label2.Width / 2);

            label3.Width = button3.Width;
            label3.Top = button3.Bottom;
            label3.Left = button3middle - (label3.Width / 2);
        }

        private void Form2_Closing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        private void ResizePictureBox()
        {
            pictureBox1.ClientSize = this.ClientSize;
        }

        private void button1Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(username, kullanıcıID); // Kullanıcı adını Form1'e parametre olarak iletiliyor

            form1.ClientSize = this.ClientSize;

            if (this.WindowState == FormWindowState.Maximized)
            {
                form1.WindowState = FormWindowState.Maximized;
            }

            this.Hide();
            form1.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormFilm formFilm = new FormFilm(username, kullanıcıID);
            formFilm.ClientSize = this.ClientSize;

            if (this.WindowState == FormWindowState.Maximized)
            {
                formFilm.WindowState = FormWindowState.Maximized;
            }

            this.Hide();
            formFilm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Yeni izlemeListesi formunu oluşturun
            izlemeListesi izlemeForm = new izlemeListesi(kullanıcıID);

            // Eğer bu formun boyutu bu formun boyutu ile aynı olmalı ise:
            izlemeForm.ClientSize = this.ClientSize;

            // Eğer bu formun durumu bu formun durumu ile aynı olmalı ise:
            if (this.WindowState == FormWindowState.Maximized)
            {
                izlemeForm.WindowState = FormWindowState.Maximized;
            }

            // Bu formu gizleyin ve yeni formu gösterin
            this.Hide();
            izlemeForm.Show();
        }

    }
}
