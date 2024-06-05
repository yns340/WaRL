using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using static WinFormsApp1.FormGirisEkrani;

namespace WinFormsApp1
{
    public partial class okumaListesi : Form
    {
        private int _kullaniciID;

        public okumaListesi(int kullaniciID)
        {
            InitializeComponent();
            _kullaniciID = kullaniciID;
            LoadWatchList();
        }

        private void LoadWatchList()
        {
            okumaListesi_Resize(this, EventArgs.Empty);

            try
            {
                // okuma listesi veritabanından alınıyor
                DataTable readingList = GetReadingListFromDatabase(_kullaniciID);

                // Veritabanından gelen okuma listesi DataGridView kontrolüne ekleniyor
                dgv.DataSource = readingList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Okuma listesi yüklenirken bir hata oluştu: " + ex.Message);
            }
        }
        private string RootDirectory() // string değer döndürülecek
        {
            DirectoryInfo directory = new DirectoryInfo(Application.StartupPath);
            return directory.Parent.Parent.Parent.Parent.FullName; // uygulama debug içinde çalıştığından en dış klasör olan .sln nin olduğu dizine dek çıktık
        }

        private string GetDatabasePath()
        {
            string dirRoot = RootDirectory();
            return Path.Combine(dirRoot, "WinFormsApp1", "database", "Database2.accdb"); // Veritabanı dosya adınızı burada belirtin
        }


        private DataTable GetReadingListFromDatabase(int kullaniciID)
        {
            DataTable readingList = new DataTable();
            string databasePath = GetDatabasePath();
            // Veritabanına bağlanma ve okuma listesi sorgusunu yürütme
            using (OleDbConnection connection = new OleDbConnection($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databasePath}"))
            {
                string query = "SELECT Kitaplar.KitapAdı " +
                               "FROM Kitaplar " +
                               "INNER JOIN okumaListesi ON Kitaplar.KitapID=okumaListesi.KitapID " +
                               "WHERE okumaListesi.KullanıcıID = @KullanıcıID";
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@KullanıcıID", kullaniciID);
                adapter.Fill(readingList);
            }

            return readingList;
        }

        private void okumaListesi_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        private void okumaListesi_Resize(object sender, EventArgs e)
        {
            panel1.Width = ClientSize.Width;
            panel1.Height = 89 * panel1.Width / 1516;

            panel1.Top = 92;
            panel1.Left = 52;
            button1.Height = panel1.Height;
            button1.Width = button1.Height;

            dgv.Top = panel1.Bottom + 50 ;
            dgv.Left = panel1.Left;

            label1.Left = panel1.Left + button1.Width;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form = new Form3(KullanıcıGirişi.KullanıcıAdı, KullanıcıGirişi.KullanıcıID); //bura doğru mu ???
            form.ClientSize = this.ClientSize;

            if (this.WindowState == FormWindowState.Maximized)
            {
                form.WindowState = FormWindowState.Maximized;
            }

            this.Hide();
            form.Show();
        }
    }
}
