using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
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

            dgv.CellContentClick += Dgv_CellContentClick;
        }

        private void Dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgv.Columns["RemoveButton"].Index && e.RowIndex >= 0)
            {
                int selectedRowIndex = e.RowIndex;
                string selectedFilmAdı = dgv.Rows[selectedRowIndex].Cells["Adı"].Value.ToString();

                DialogResult result = MessageBox.Show($"'{selectedFilmAdı}' adlı filmi çıkarmak istediğinize emin misiniz?", "Confirm Removal", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    int selectedFilmDiziID = GetFilmDiziIDFromDatabase(selectedFilmAdı);
                    RemoveFromWatchList(selectedFilmDiziID, _kullaniciID);
                    LoadWatchList();
                }
            }
        }

        private int GetFilmDiziIDFromDatabase(string filmAdı)
        {
            int filmDiziID = -1;
            string databasePath = GetDatabasePath();

            using (OleDbConnection connection = new OleDbConnection($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databasePath}"))
            {
                string query = "SELECT Kimlik FROM filmdizilistesi WHERE Adı = @FilmAdı";
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("@FilmAdı", filmAdı);

                connection.Open();
                var result = command.ExecuteScalar();
                if (result != null)
                {
                    filmDiziID = Convert.ToInt32(result);
                }
            }

            return filmDiziID;
        }

        private void RemoveFromWatchList(int filmDiziID, int kullaniciID)
        {
            string databasePath = GetDatabasePath();

            using (OleDbConnection connection = new OleDbConnection($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databasePath}"))
            {
                string query = "DELETE FROM izlemeListesi WHERE FilmDiziID = @FilmDiziID AND KullanıcıID = @KullanıcıID";
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("@FilmDiziID", filmDiziID);
                command.Parameters.AddWithValue("@KullanıcıID", kullaniciID);

                connection.Open();
                command.ExecuteNonQuery();
            }
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

                if (!dgv.Columns.Contains("RemoveButton"))
                {
                    DataGridViewImageColumn removeButtonColumn = new DataGridViewImageColumn
                    {
                        Name = "RemoveButton",
                        HeaderText = "Listeden Çıkar", // Header text with spaces
                        //Image = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "trash.jpg")),
                        //ImageLayout = DataGridViewImageCellLayout.Zoom
                    };
                    dgv.Columns.Add(removeButtonColumn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("İzleme listesi yüklenirken bir hata oluştu: " + ex.Message);
            }
        }

        private string RootDirectory()
        {
            DirectoryInfo directory = new DirectoryInfo(Application.StartupPath);
            return directory.Parent.Parent.Parent.Parent.FullName;
        }

        private string GetDatabasePath()
        {
            string dirRoot = RootDirectory();
            return Path.Combine(dirRoot, "WinFormsApp1", "database", "Database2.accdb");
        }

        private DataTable GetWatchListFromDatabase(int kullaniciID)
        {
            DataTable watchList = new DataTable();
            string databasePath = GetDatabasePath();

            using (OleDbConnection connection = new OleDbConnection($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databasePath}"))
            {
                string query = "SELECT fdl.Adı, fdl.Yıl, fdl.Yapımcı, fdl.Türü, fdl.Puan " +
                               "FROM filmdizilistesi fdl " +
                               "INNER JOIN izlemeListesi il ON fdl.Kimlik = il.FilmDiziID " +
                               "WHERE il.KullanıcıID = @KullanıcıID";
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
            Form2 form = new Form2(KullanıcıGirişi.KullanıcıAdı, KullanıcıGirişi.KullanıcıID);
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
