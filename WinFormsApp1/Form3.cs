using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System;
using System.Windows.Forms;
using static WinFormsApp1.FormGirisEkrani;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WinFormsApp1
{
    public partial class Form3 : Form
    {
        private string username;
        private int kullanıcıID;

        // Form2'nin parametre alan yapılandırıcısı
        public Form3(string username, int kullanıcıID)
        {
            InitializeComponent();
            this.username = username; // Gelen kullanıcı adını sakla
            this.kullanıcıID = kullanıcıID;
            this.FormClosing += new FormClosingEventHandler(Form3_Closing);
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            ResizePictureBox();
            button1.Height = label1.Height;
            button1.Width = label1.Height;
            Form3_Resize(this, EventArgs.Empty);
        }

        private void Form3_Closing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        private void Form3_Resize(object sender, EventArgs e)
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

        private void ResizePictureBox()
        {
            pictureBox1.ClientSize = this.ClientSize;
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(KullanıcıGirişi.KullanıcıAdı, KullanıcıGirişi.KullanıcıID);
            form1.ClientSize = this.ClientSize;

            if (this.WindowState == FormWindowState.Maximized)
            {
                form1.WindowState = FormWindowState.Maximized;
            }

            this.Hide();
            form1.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            FormKitap formkitap = new FormKitap(username, kullanıcıID);
            formkitap.ClientSize = this.ClientSize;

            if (this.WindowState == FormWindowState.Maximized)
            {
                formkitap.WindowState = FormWindowState.Maximized;
            }

            this.Hide();
            formkitap.Show();
        }
            private void button3_Click(object sender, EventArgs e)
            {
                // Yeni izlemeListesi formunu oluşturun
                okumaListesi okumaForm = new okumaListesi(kullanıcıID);

                // Eğer bu formun boyutu bu formun boyutu ile aynı olmalı ise:
                okumaForm.ClientSize = this.ClientSize;

                // Eğer bu formun durumu bu formun durumu ile aynı olmalı ise:
                if (this.WindowState == FormWindowState.Maximized)
                {
                okumaForm.WindowState = FormWindowState.Maximized;
                }

                // Bu formu gizleyin ve yeni formu gösterin
                this.Hide();
                okumaForm.Show();
            }

        }
    }
