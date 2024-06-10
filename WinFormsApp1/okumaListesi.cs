using Nest;
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Resources;
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
            LoadReadingList();

            dgv.CellContentClick += Dgv_CellContentClick;
        }

        private void LoadReadingList()
        {
            okumaListesi_Resize(this, EventArgs.Empty);

            try
            {
                
                DataTable readingList = GetReadingListFromDatabase(_kullaniciID);

               
                dgv.DataSource = readingList;

                if (!dgv.Columns.Contains("RemoveButton"))
                {
                    
                    DataGridViewImageColumn removeButtonColumn = new DataGridViewImageColumn
                    {
                        Name = "RemoveButton",
                        HeaderText = "Listeden Çıkar",
                        
                        //Image = WinFormsApp1.Properties.Resources.trash,
                        Width = 50
                    };
                    dgv.Columns.Add(removeButtonColumn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Okuma listesi yüklenirken bir hata oluştu: " + ex.Message);
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

        private DataTable GetReadingListFromDatabase(int kullaniciID)
        {
            DataTable readingList = new DataTable();
            string databasePath = GetDatabasePath();

            // Veritabanına bağlanma ve okuma listesi sorgusunu yürütme
            using (OleDbConnection connection = new OleDbConnection($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databasePath}"))
            {
                string query = "SELECT Kitaplar.KitapAdı,Kitaplar.Yazar,Kitaplar.Türler " +
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

            dgv.Top = panel1.Bottom + 50;
            dgv.Left = panel1.Left;

            label1.Left = panel1.Left + button1.Width;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form = new Form3(KullanıcıGirişi.KullanıcıAdı, KullanıcıGirişi.KullanıcıID);
            form.ClientSize = this.ClientSize;

            if (this.WindowState == FormWindowState.Maximized)
            {
                form.WindowState = FormWindowState.Maximized;
            }

            this.Hide();
            form.Show();
        }

        private void Dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgv.Columns["RemoveButton"].Index && e.RowIndex >= 0)
            {
                int selectedRowIndex = e.RowIndex;
                string selectedKitapAdı = dgv.Rows[selectedRowIndex].Cells["KitapAdı"].Value.ToString();

                DialogResult result = MessageBox.Show($"'{selectedKitapAdı}' adlı kitabı çıkarmak istediğinize emin misiniz?", "Confirm Removal", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    int selectedKitapID = GetKitapIDFromDatabase(selectedKitapAdı);
                    RemoveFromReadingList(selectedKitapID, _kullaniciID);
                    LoadReadingList();
                }
            }
        }

        private int GetKitapIDFromDatabase(string kitapAdı)
        {
            int kitapID = -1;
            string databasePath = GetDatabasePath();

            using (OleDbConnection connection = new OleDbConnection($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databasePath}"))
            {
                string query = "SELECT KitapID FROM Kitaplar WHERE KitapAdı = @KitapAdı";
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("@KitapAdı", kitapAdı);

                connection.Open();
                var result = command.ExecuteScalar();
                if (result != null)
                {
                    kitapID = Convert.ToInt32(result);
                }
            }

            return kitapID;
        }

        private void RemoveFromReadingList(int kitapID, int kullaniciID)
        {
            string databasePath = GetDatabasePath();

            using (OleDbConnection connection = new OleDbConnection($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databasePath}"))
            {
                string query = "DELETE FROM okumaListesi WHERE KitapID = @KitapID AND KullanıcıID = @KullanıcıID";
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("@KitapID", kitapID);
                command.Parameters.AddWithValue("@KullanıcıID", kullaniciID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void okumaListesi_Load(object sender, EventArgs e)
        {

        }
    }
}
