using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

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

        private DataTable GetReadingListFromDatabase(int kullaniciID)
        {
            DataTable readingList = new DataTable();

            // Veritabanına bağlanma ve okuma listesi sorgusunu yürütme
            using (OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database2.accdb"))
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
    }
}
