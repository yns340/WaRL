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
using System.Net;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace WinFormsApp1
{
    public partial class FormUserKayit : Form
    {

        public FormUserKayit()  
        {
            InitializeComponent();
        }

        private void FormUserKayit_Load(object sender, EventArgs e)
        {
            FormUserKayit_Resize(this, EventArgs.Empty);
        }

        private void FormUserKayit_Resize(object sender, EventArgs e)
        {
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            label2.Left = (this.ClientSize.Width - label2.Width) / 2;
            button1.Left = (this.ClientSize.Width - button1.Width) / 2;
            textBox1.Left = (this.ClientSize.Width - textBox1.Width) / 2;
            textBox2.Left = (this.ClientSize.Width - textBox2.Width) / 2;
            textBox1.Width = label2.Width;
            textBox2.Width = textBox1.Width;
            label3.Left = textBox1.Left;
            label4.Left = textBox2.Left;
            button2.Width = button1.Width;
            button2.Left = button1.Left;

        }
        private string RootDirectory() // Kullanıcının .sln uygulamasının olduğu dizini almasını sağlayan method
        {
            DirectoryInfo directory = new DirectoryInfo(Application.StartupPath);
            return directory.Parent.Parent.Parent.Parent.FullName; // uygulama debug içinde çalıştığından en dış klasör olan .sln nin olduğu dizine dek çıkıldı.
        }

        private string GetDatabasePath() // RootDirectoryle alınan yol ile istediğimiz klasorü birleştirmemizi sağlayan method
        {
            string dirRoot = RootDirectory();
            return Path.Combine(dirRoot, "WinFormsApp1", "database", "Database2.accdb"); // Veritabanı dosya adımız ile rootdirectoryden geleni birleştirildi.
        }


        private void button1_Click(object sender, EventArgs e) // Kayıt işlemini gerçekleştiren butona basıldığında gerçekleşenler
        {
            try
            {
                string databasePath = GetDatabasePath(); // Dinamik veritabanı yolunun bağlantı ıcın kullanılması amacıyla değişkene atandı.
                using (OleDbConnection baglanti = new OleDbConnection($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databasePath}")) // veritabanı bağlantısının gerçekleştirilmesi
                {
                    baglanti.Open();
                    string sorgu = "INSERT INTO kullaniciislemleri (userName,[password]) VALUES (@ad,@sifre)";   // Veritabanına girilen parametrelerin kaydedilmesi 
                    using (OleDbCommand komut = new OleDbCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@ad", textBox1.Text);
                        komut.Parameters.AddWithValue("@sifre", textBox2.Text);
                        komut.ExecuteNonQuery();
                    }
                    baglanti.Close();
                }

                MessageBox.Show("kullanıcı eklendi!!");
                FormGirisEkrani formGiris = new FormGirisEkrani();  // kulllanıcı eklendıkten sonra giriş ekranıa geri donüldü
                formGiris.Show();
                this.Dispose();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e) // kullanıcı vazgeçerse herhangi bir kayıt işlemi yapmadan geri donülen vazgeç butonu
        {
            FormGirisEkrani formgiris = new FormGirisEkrani();
            if (this.WindowState == FormWindowState.Maximized)
            {
                formgiris.WindowState = FormWindowState.Maximized;
            }

            this.Hide();
            formgiris.Show();

        }

        private void FormUserKayit_FormClosing(object sender, FormClosingEventArgs e) //Formun doğru şekilde kapatılması için
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

    }
}
