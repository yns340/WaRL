using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static WinFormsApp1.FormGirisEkrani;

namespace WinFormsApp1
{
    public partial class FormKitap : Form
    {
        private string _username;
        private int _kullanıcıID;
        private DataTable kitaplar;

        public FormKitap(string username, int kullanıcıID)
        {
            InitializeComponent();
            this._username = username;
            this._kullanıcıID = kullanıcıID;
            // Checkbox'ların durum değişikliklerini izlemek için olayları ata
            checkBox1.CheckedChanged += CheckBox_CheckedChanged;
            checkBox2.CheckedChanged += CheckBox_CheckedChanged;
            // Diğer checkboxlar için aynı şekilde devam edebilirsiniz
        }

        private void FormKitap_Load(object sender, EventArgs e)
        {
            LoadKitap();
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

        private void LoadKitap()
        {
            string databasePath = GetDatabasePath();
            using (OleDbConnection connection = new OleDbConnection($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databasePath}"))
            {
                string query = "SELECT * FROM Kitaplar order by KitapID";
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection);
                kitaplar = new DataTable();
                adapter.Fill(kitaplar);
                DisplayKitaplar(kitaplar);
            }
        }

        private void DisplayKitaplar(DataTable kitaplar)
        {
            // Seçilen türleri al
            var selectedGenres = GetSelectedGenres();

            // Tüm panellerin temizlenmesi
            foreach (Control control in this.Controls.OfType<Panel>().Where(panel => panel != panel1).ToList())
            {
                this.Controls.Remove(control);
                control.Dispose();
            }

            int panelWidth = ClientSize.Width / 3;
            int panelHeight = ClientSize.Height - panel1.Height;

            int rowIndex = 0;
            int columnIndex = 0;

            foreach (DataRow row in kitaplar.Rows)
            {
                // Seçilen türlere uygun kitaplar varsa sadece o türlere ait panelleri güncelle
                if (selectedGenres.All(genre => row["Türler"].ToString().Contains(genre)))
                {
                    Panel panel = new Panel
                    {
                        Width = panelWidth,
                        Height = panelHeight,
                        Left = columnIndex * panelWidth,
                        Top = panel1.Height + rowIndex * panelHeight,
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = Color.White,
                    };

                    string imageName = row["Kapak"].ToString();
                    string dirRoot = RootDirectory();
                    string imagePath = Path.Combine(dirRoot, "WinFormsApp1", "Kapaklar", imageName);

                    PictureBox pictureBox = new PictureBox
                    {
                        ImageLocation = imagePath,
                        BackColor = Color.Red,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Width = panelWidth - 150,
                        Left = 75,
                        Top = 50,
                        Height = panelHeight - 300,
                    };
                    panel.Controls.Add(pictureBox);

                    Label label = new Label
                    {
                        Text = $"Kitabın Adı: {row["KitapAdı"]}\n" +
                               $"Kitabın Türleri: {row["Türler"]}\n" +
                               $"Kitabın Yazarı: {row["Yazar"]}\n" +
                               $"Kitabın Puanı: {row["Puan"]}\n",
                        AutoSize = true,
                        Location = new Point(75, pictureBox.Bottom + 20),
                    };

                    panel.Controls.Add(label); //label in uzunluğunun botton tarafından bilinmesi için önceden panele ekledik

                    Button button = new Button
                    {
                        Width = pictureBox.Width,
                        Height = 75,
                        Text = "Listeye Ekle",
                        Location = new Point(75, label.Bottom + 20),
                        Tag = row["KitapID"],
                    };
                    button.Click += ButtonClick;

                    panel.Controls.Add(button);
                    this.Controls.Add(panel);

                    columnIndex++;
                    if (columnIndex == 3)
                    {
                        columnIndex = 0;
                        rowIndex++;
                    }
                }
            }

            this.HorizontalScroll.Enabled = false;
            this.VerticalScroll.Enabled = true;
        }


        private void ButtonClick(object sender, EventArgs e)
        {
            Button button = sender as Button;

            int KitapID = Convert.ToInt32(button.Tag);
            int kullaniciID = GetCurrentUserID();

            AddKitapToWatchList(KitapID, kullaniciID);
        }

        private int GetCurrentUserID()
        {
            return _kullanıcıID;
        }

        private void AddKitapToWatchList(int KitapID, int kullaniciID)
        {
            try
            {
                string databasePath = GetDatabasePath();
                using (OleDbConnection connection = new OleDbConnection($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databasePath}"))
                {
                    string kontrolsorgu = "SELECT COUNT(*) FROM okumaListesi WHERE KullanıcıID=@KullanıcıID AND KitapID=@KitapID";
                    using (OleDbCommand sorgukmt = new OleDbCommand(kontrolsorgu, connection))
                    {
                        sorgukmt.Parameters.AddWithValue("@KullanıcıID,", kullaniciID);
                        sorgukmt.Parameters.AddWithValue("@KitapID,", KitapID);
                        connection.Open();

                        int sayı = (int)sorgukmt.ExecuteScalar();
                        if (sayı > 0)
                        {
                            MessageBox.Show("Bu kitap zaten okuma listenizde bulunuyor.");
                        }
                        else
                        {
                            connection.Close();
                            string query = "INSERT INTO okumaListesi (KullanıcıID, KitapID) VALUES (@KullanıcıID, @KitapID)";
                            using (OleDbCommand command = new OleDbCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@KullanıcıID", kullaniciID);
                                command.Parameters.AddWithValue("@KitapID", KitapID);
                                connection.Open();
                                command.ExecuteNonQuery();
                            }

                            MessageBox.Show("Okuma listenize eklendi");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FormKitap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
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

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FilterBooksByGenres(); // Checkbox durumları değiştiğinde kitapları filtrele
        }

        private void FilterBooksByGenres()
        {
            var selectedGenres = GetSelectedGenres(); // Seçilen türleri al

            // Seçilen türlere tam olarak eşleşen kitapları filtrele
            var filteredRows = kitaplar.AsEnumerable()
                                        .Where(row => selectedGenres.All(genre => row["Türler"].ToString().Contains(genre)))
                                        .ToList();

            if (filteredRows.Any())
            {
                // Filtrelenmiş satırları kullanarak kitapları yeniden görüntüle
                DisplayKitaplar(filteredRows.CopyToDataTable());
            }
            else
            {
                MessageBox.Show("Seçilen türe uygun kitap bulunamadı.");
                // Tüm kitapları göster
                DisplayKitaplar(kitaplar);
            }
        }



        private List<string> GetSelectedGenres()
        {
            List<string> selectedGenres = new List<string>();

            // Panel1 içindeki tüm kontrolleri kontrol et
            foreach (Control control in panel1.Controls)
            {
                // Kontrol bir checkbox ise ve işaretliyse, türünü seçilen türler listesine ekle
                if (control is CheckBox checkBox && checkBox.Checked)
                {
                    selectedGenres.Add(checkBox.Text);
                }
            }

            return selectedGenres;
        }
    }
}