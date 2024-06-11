using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class FormGirisEkrani : Form
    {
        public static class KullanıcıGirişi  // Girilen kullanıcının gerekli bilgilerini tutmak amacıyla oluşturulmuş class
        {
            public static string KullanıcıAdı { get; set; }
            public static int KullanıcıID { get; set; }
        }

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

        private void button1_Click(object sender, EventArgs e)  // Giriş yapmak için butona basıldığında gerçekleşen işlemler
        {
            string databasePath = GetDatabasePath();
            using (OleDbConnection baglanti = new OleDbConnection($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databasePath}")) //veritabanı bağlantısının gerçekleştirilmesi
            {
                baglanti.Open();
                using (OleDbCommand sorgu = new OleDbCommand("select userName,password from kullaniciislemleri where userName=@ad and password=@sifre", baglanti)) //Girilen kullanıcı  bilgileri ile veritabanındaki bilgilerin uyumluluğu denetlendi.
                {
                    sorgu.Parameters.AddWithValue("@ad", textBox1.Text);
                    sorgu.Parameters.AddWithValue("@sifre", textBox2.Text);
                    using (OleDbDataReader dr = sorgu.ExecuteReader())
                    {
                        if (dr.Read()) // OleDbDataReader ile sorgu gerçekleştirildi.
                        {
                            string username = textBox1.Text;
                            string password = textBox2.Text;

                            if (IsValidUser(username, password, out int userId)) // kullanıcı girişi başarılı oldugunda kullanıcının gerekli bilgileri kayıt altına alındı ve diğer formlara aktarıldı.
                            {
                                KullanıcıGirişi.KullanıcıAdı = username;
                                KullanıcıGirişi.KullanıcıID = userId;
                                Form1 form1 = new Form1(username, userId);

                                if (this.WindowState == FormWindowState.Maximized)
                                {
                                    form1.WindowState = FormWindowState.Maximized;
                                }

                                form1.Show();
                                this.Hide();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Yanlış kullanıcı adı veya parolası. Lütfen tekrar deneyin.");
                        }
                    }
                }
            }
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

        private bool IsValidUser(string username, string password, out int userId) // Kullanıcının girdiği bilgilerle veritabanındaki bilgilerin doğruluğunu kontrol eden method.
        {
            userId = -1;
            string query = "SELECT Kimlik FROM kullaniciislemleri WHERE Username = ? AND Password = ?";  //Gerekli bilgileri veritabanından cekme işlemi
            string databasePath = GetDatabasePath();

            using (OleDbConnection connection = new OleDbConnection($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databasePath}")) // Veritabanı bağlantısı 
            {
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();
                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        userId = Convert.ToInt32(result);    // Kullanıcı girşi gerçekleştikten sonra hangi kullanıcı idye sahip olunduğunun geri döndürülmesi.
                        return true;
                    }
                }
            }
            return false;
        }


        private void button2_Click(object sender, EventArgs e) // Kayıt olmak için kayıt ol butonuna basıldıktan sonra kayıtol formuna geçiş yapıldı.
        {
            FormUserKayit form = new FormUserKayit();

            if (this.WindowState == FormWindowState.Maximized)
            {
                form.WindowState = FormWindowState.Maximized;
            }

            form.Show();
            this.Hide();
        }

        private void FormGirisEkrani_FormClosing(object sender, FormClosingEventArgs e) //formun doğru şekilde kapanması sağlandı.
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }
    }
}
