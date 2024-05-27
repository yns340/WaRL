using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using static System.Windows.Forms.DataFormats;

namespace WinFormsApp1
{
    public partial class FormGirisEkrani : Form
    {
        public FormGirisEkrani()
        {
            InitializeComponent();
        }

        private void FormGirisEkrani_Load(object sender, EventArgs e)
        {
            FormGirisEkrani_Resize(this, EventArgs.Empty);

        }

        private void FormGirisEkrani_Resize(object sender, EventArgs e)
        {
            pictureBox1.Width = (ClientSize.Height * 315) / 426;
            pictureBox1.Height = ClientSize.Height;

            label3.Left = pictureBox1.Width + (ClientSize.Width - pictureBox1.Width - label3.Width) / 2;
            textBox1.Left = pictureBox1.Width + (ClientSize.Width - pictureBox1.Width - textBox1.Width) / 2;
            textBox2.Left = textBox1.Left;
            label1.Left = textBox1.Left;
            label2.Left = textBox1.Left;
            button1.Left = textBox1.Left;
            button2.Left = pictureBox1.Width + (textBox1.Width - button2.Width) + (ClientSize.Width - pictureBox1.Width - textBox1.Width) / 2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source = Database2.accdb");
            baglanti.Open();
            OleDbCommand sorgu = new OleDbCommand("select userName,password from kullaniciislemleri where userName=@ad and password=@sifre", baglanti);
            sorgu.Parameters.AddWithValue("@ad", textBox1.Text);
            sorgu.Parameters.AddWithValue("@sifre", textBox2.Text);
            OleDbDataReader dr;
            dr = sorgu.ExecuteReader();

            if (dr.Read())
            {
                Form1 form = new Form1();

                if (this.WindowState == FormWindowState.Maximized)
                {
                    form.WindowState = FormWindowState.Maximized;
                }

                form.Show();
                this.Hide();
            }

            else
            {
                MessageBox.Show("Yanlış kullanıcı adı veya parolası. Lütfen tekrar deneyin.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormUserKayit form = new FormUserKayit();
            form.Show();
            this.Hide();
        }
    }
}
