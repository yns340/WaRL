using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using static WinFormsApp1.FormGirisEkrani;

namespace WinFormsApp1
{
    public partial class izlemeListesi : Form
    {
        private int _kullaniciID;

        public izlemeListesi(int kullaniciID)
        {
            InitializeComponent();
            _kullaniciID = kullaniciID;
            LoadWatchList();
        }

        private void LoadWatchList()
        {
            izlemeListesi_Resize(this, EventArgs.Empty);

            try
            {
                // İzleme listesi veritabanından alınıyor
                DataTable watchList = GetWatchListFromDatabase(_kullaniciID);

                // Veritabanından gelen izleme listesi DataGridView kontrolüne ekleniyor
                dgv.DataSource = watchList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("İzleme listesi yüklenirken bir hata oluştu: " + ex.Message);
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


        private DataTable GetWatchListFromDatabase(int kullaniciID)
        {
            DataTable watchList = new DataTable();
            string databasePath = GetDatabasePath();

            // Veritabanına bağlanma ve izleme listesi sorgusunu yürütme
            using (OleDbConnection connection = new OleDbConnection($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databasePath}"))
            {
                string query = "SELECT filmdizilistesi.adi " +
                               "FROM filmdizilistesi " +
                               "INNER JOIN izlemeListesi ON filmdizilistesi.Kimlik=izlemeListesi.FilmDiziID " +
                               "WHERE izlemeListesi.KullanıcıID = @KullanıcıID";
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@KullanıcıID", kullaniciID);
                adapter.Fill(watchList);
            }

            return watchList;
        }

        private void izlemeListesi_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        private void izlemeListesi_Resize(object sender, EventArgs e)
        {
            panel1.Width = ClientSize.Width;
            panel1.Height = 89 * panel1.Width / 1516;

            panel1.Top = 92;
            panel1.Left = 52;
            button1.Height = panel1.Height;
            button1.Width = button1.Height;

            dgv.Top = panel1.Bottom + 50;
            dgv.Left = panel1.Left;

            label1.Left = panel1.Left + button1.Width;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2(KullanıcıGirişi.KullanıcıAdı, KullanıcıGirişi.KullanıcıID); //bura doğru mu ???
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
