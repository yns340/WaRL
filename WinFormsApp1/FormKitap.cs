﻿using System;
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

        public FormKitap(string username, int kullanıcıID)//KullanıcID ve username parametre olarak alınır.
        {
            InitializeComponent();
            this._username = username;//Yukarıda tanımlanan değişkenlere alınan parametreler eşitlenşr.
            this._kullanıcıID = kullanıcıID;
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

        private void DisplayKitaplar(DataTable kitaplar)//Bu fonksiyonda paneller veritabanından dinamik olarak çekilip forma eklenir.
        {
            var selectedGenres = GetSelectedGenres(); //checkboxlarla seçilen türler alındı.

            foreach (Control control in this.Controls.OfType<Panel>().Where(panel => panel != panel1).ToList()) // panel1 haricindeki diğer panelleri temizlenmesi sağlandı.
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
                if (selectedGenres.All(genre => row["Türler"].ToString().Contains(genre))) //seçilen türlere göre uyumlu kitapların getirilmesi için kontrol.
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
                               $"Kitabın Yazarı: {row["Yazar"]}\n",

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
           
            int KitapID = Convert.ToInt32(button.Tag); // KitapId ile button tag'i birbirine eşitlenerek butona tıklandığında KitapID'nin  alınması sağlanır.
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
                    //Ekli olan kitabın tekrar eklenmesi engellenir.
                    string kontrolsorgu = "SELECT COUNT(*) FROM okumaListesi WHERE KullanıcıID=@KullanıcıID AND KitapID=@KitapID";
                    using (OleDbCommand sorgukmt = new OleDbCommand(kontrolsorgu, connection))
                    {
                        sorgukmt.Parameters.AddWithValue("@KullanıcıID,", kullaniciID);
                        sorgukmt.Parameters.AddWithValue("@KitapID,", KitapID);
                        connection.Open();

                        int sayı = (int)sorgukmt.ExecuteScalar();//Eklenen kitap, sayı değişkenine atanır.Sayı>0 ise kitap ekli diye uyarı verilir
                        if (sayı > 0)
                        {
                            MessageBox.Show("Bu kitap zaten okuma listenizde bulunuyor.");
                        }
                        else
                        {
                            connection.Close();
                            //Kitap ekli değilse kullanıcıId ve kitapID aracılığıyla okumalistesi tablosuna eklenir.
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
            FilterBooksByGenres(); // Checkbox durumları değiştiğinde kitapları filtrelemek için metodun cağrılması.
        }

        private void FilterBooksByGenres()
        {
            var selectedGenres = GetSelectedGenres(); // Seçilen türleri al

            // Seçilen türlere tam olarak eşleşen kitapları filtrele
            var filteredRows = kitaplar.AsEnumerable() // Seçili türleri filtrelemek için yeni bir değişken
                                        .Where(row => selectedGenres.All(genre => row["Türler"].ToString().Contains(genre)))
                                        .ToList();

            if (filteredRows.Any())
            {
                DisplayKitaplar(filteredRows.CopyToDataTable()); // Filtrelenmiş satırları kullanarak kitapları yeniden görüntüle
            }
            else
            {
                MessageBox.Show("Seçilen türe uygun kitap bulunamadı.");  // Türe uygun fılm bulunmadıktan sonra tüm filmleri göster
                DisplayKitaplar(kitaplar);
            }
        }



        private List<string> GetSelectedGenres() // Checkboxlardan seçili olanların textlerini alarak aradığımız türleri almak amacıyla yazılmış method.
        {
            List<string> selectedGenres = new List<string>();

            foreach (Control control in panel1.Controls) // Tüm checkboxlar panel1 içerisinde bulunuyor panel1dekı checkboxlardan secılı olanlarının metınlerını eklemek ıcın dongu.
            {
                if (control is CheckBox checkBox && checkBox.Checked)
                {
                    selectedGenres.Add(checkBox.Text);
                }
            }

            return selectedGenres;
        }
    }
}