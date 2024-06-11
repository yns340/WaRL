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
    public partial class FormFilm : Form
    {
        private string _username;
        private int _kullanıcıID;
        private DataTable films; //datatable türünde bir değişken tanımlandı

        public FormFilm(string username, int kullanıcıID)
        {
            InitializeComponent();
            this._username = username;
            this._kullanıcıID = kullanıcıID;
        }

        private void FormFilm_Load(object sender, EventArgs e)
        {
            LoadFilms(); //Form yüklenince ilk bu foksiyon çağrılıyor.
        }

        private string RootDirectory()
        {
            DirectoryInfo directory = new DirectoryInfo(Application.StartupPath);//başlangıç dosyasının(.exe uzantılı) bulunduğu yerin tam konumu
            return directory.Parent.Parent.Parent.Parent.FullName;//başlangıç konumundan .sln dosya uzantılı dosyanın bulunduğu klasöre dek çıkıldı
        }

        private string GetDatabasePath()
        {
            string dirRoot = RootDirectory(); //başlangıç konumunun belirlenmesi
            return Path.Combine(dirRoot, "WinFormsApp1", "database", "Database2.accdb");
        }

        private void LoadFilms()
        {
            string databasePath = GetDatabasePath(); //database in yolu bulunmalı
            using (OleDbConnection connection = new OleDbConnection($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databasePath}"))// data kaynağı olarak verildi bulunan konum
            {
                string query = "SELECT * FROM filmdizilistesi ORDER BY Kimlik"; //konumu verilen database in istenilen tablosu istenilen sıra ile çekildi
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection);//bir adapter ile bu bağlantı ve istek birleştirildi
                films = new DataTable(); //datatable türünde değişken oluşturuldu
                adapter.Fill(films); //adapter ile bu datatable ın içerisi dolduruldu
                DisplayFilms(films); //films datatable ı parametre verilerek bu fonksiyon çağrıldı.
            }
        }

        private void DisplayFilms(DataTable films) //filmleri dinamik olarak oluşturan fonksiyon
        {
            
            var selectedGenres = GetSelectedGenres(); //checkboxlarla seçilen türler alındı.

            
            foreach (Control control in this.Controls.OfType<Panel>().Where(panel => panel != panel1).ToList()) // panel1 haricindeki diğer panelleri temizlenmesi sağlandı.
            {
                this.Controls.Remove(control);
                control.Dispose();
            }

            int panelWidth = ClientSize.Width / 3;
            int panelHeight = ClientSize.Height - panel1.Height;

            for (int i = 0; i < films.Rows.Count; i++) //film sayısı kadar tekrar edecek
            {
                DataRow row = films.Rows[i]; //Satırları satır bilgisi olarak tutmak için 

                
                if (selectedGenres.All(genre => row["Türü"].ToString().Contains(genre))) //seçilen türlere göre uyumlu filmlerin getirilmesi için kontrol.
                {
                    Panel panel = new Panel
                    {
                        Width = panelWidth,
                        Height = panelHeight,
                        Left = (i % 3) * panelWidth,
                        Top = panel1.Height + (i / 3) * panelHeight,
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = Color.White,
                    };

                    string imageName = row["poster"].ToString(); //her bir filmin kendi poster bilgileri
                    string dirRoot = RootDirectory();
                    string imagePath = Path.Combine(dirRoot, "WinFormsApp1", "filmposter", imageName); //.sln ye kadar inilen yerden istenilen yere kadar yol verildi

                    PictureBox pictureBox = new PictureBox
                    {
                        ImageLocation = imagePath,
                        BackColor = Color.Red,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Width = panelWidth - 150,
                        Left = 75,
                        Top = 50,
                        Height = panelHeight - 350,
                    };

                    panel.Controls.Add(pictureBox); //sonradan verilecek konumlarda sıkıntı çıkmaması için önceden panele eklendi

                    Label label = new Label
                    {
                        Text = $"{row["filmMiDiziMi"]}\n" +
                               $"{row["filmMiDiziMi"]} Türü: {row["Türü"]}\n" +
                               $"{row["filmMiDiziMi"]} Adı: {row["Adı"]}\n" +
                               $"{row["filmMiDiziMi"]} Yılı: {row["Yıl"]}\n" +
                               $"{row["filmMiDiziMi"]} Yapımcısı: {row["Yapımcı"]}\n" +
                               $"{row["filmMiDiziMi"]} Puanı: {row["Puan"]}\n",
                        AutoSize = true,
                        Location = new Point(75, pictureBox.Bottom + 20),
                    };

                    panel.Controls.Add(label); //sonradan verilecek konumlarda sıkıntı çıkmaması için önceden panele eklendi

                    Button button = new Button
                    {
                        Width = pictureBox.Width,
                        Height = 50,
                        Text = "Listeye Ekle",
                        Location = new Point(75, label.Bottom + 20),
                        Tag = row["Kimlik"],
                    };
                    button.Click += ButtonClick;

                    panel.Controls.Add(button); //son olarak panele eklendi
                    this.Controls.Add(panel); //panel forma kontrol olarak eklendi.
                }
            }

            this.HorizontalScroll.Enabled = false;
            this.VerticalScroll.Enabled = true;
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            Button button = sender as Button;

            int filmID = Convert.ToInt32(button.Tag);
            int kullaniciID = GetCurrentUserID();

            AddFilmToWatchList(filmID, kullaniciID);
        }

        private int GetCurrentUserID()
        {
            return _kullanıcıID;
        }

        private void AddFilmToWatchList(int filmID, int kullaniciID)
        {
            try
            {
                string databasePath = GetDatabasePath();
                using (OleDbConnection connection = new OleDbConnection($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databasePath}"))
                {
                    string kontrolsorgu = "SELECT COUNT(*) FROM izlemeListesi WHERE KullanıcıID=@KullanıcıID AND FilmDiziID=@FilmDiziID";
                    using (OleDbCommand kontrolkmt = new OleDbCommand(kontrolsorgu, connection))
                    {
                        kontrolkmt.Parameters.AddWithValue("@KullanıcıID", kullaniciID);
                        kontrolkmt.Parameters.AddWithValue("@FilmDiziID", filmID);
                        connection.Open();
                        int sayı = (int)kontrolkmt.ExecuteScalar();
                        if (sayı > 0)
                        {
                            MessageBox.Show("Bu film zaten listenizde bulunuyor");
                        }
                        else
                        {
                            connection.Close();
                            string query = "INSERT INTO izlemeListesi (KullanıcıID, FilmDiziID) VALUES (@KullanıcıID, @FilmDiziID)";
                            using (OleDbCommand command = new OleDbCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@KullanıcıID", kullaniciID);
                                command.Parameters.AddWithValue("@FilmDiziID", filmID);
                                connection.Open();
                                command.ExecuteNonQuery();
                            }

                            MessageBox.Show("İzleme listenize eklendi");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FormFilm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
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

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FilterFilmsByGenres(); // Checkbox durumları değiştiğinde filmleri filtrelemek için metodun cağrılması.
        }

        private void FilterFilmsByGenres()
        {
            var selectedGenres = GetSelectedGenres(); // Seçilen türleri al

            var filteredRows = films.AsEnumerable()   // Seçili türleri filtrelemek için yeni bir değişken
                                     .Where(row => selectedGenres.All(genre => row["Türü"].ToString().Contains(genre))) 
                                     .ToList();

            if (filteredRows.Any())
            {
                DisplayFilms(filteredRows.CopyToDataTable());      // Filtrelenmiş satırları kullanarak filmleri yeniden görüntüle
            }
            else
            {
                MessageBox.Show("Seçilen türe uygun film bulunamadı.");     // Türe uygun fılm bulunmadıktan sonra tüm filmleri göster
                DisplayFilms(films);
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
