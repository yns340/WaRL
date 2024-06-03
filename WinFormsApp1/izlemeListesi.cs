using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

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

        private DataTable GetWatchListFromDatabase(int kullaniciID)
        {
            DataTable watchList = new DataTable();

            // Veritabanına bağlanma ve izleme listesi sorgusunu yürütme
            using (OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database2.accdb"))
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
    }
}
