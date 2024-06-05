using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;
using static WinFormsApp1.FormGirisEkrani;

namespace WinFormsApp1
{
    public partial class FormFilm : Form
    {
        private string _username;
        private int _kullanıcıID;

        public FormFilm(string username, int kullanıcıID)
        {
            InitializeComponent();
            this._username = username;
            this._kullanıcıID = kullanıcıID;
        }

        private void FormFilm_Load(object sender, EventArgs e)
        {
            LoadFilms();
            
        }

        private string RootDirectory() //string değer döndürülecek
        {
            DirectoryInfo directory = new DirectoryInfo(Application.StartupPath);
            return directory.Parent.Parent.Parent.Parent.FullName; //uygulama debug içinde çalıştığından en dış klasör olan .sln nin olduğu dizine dek çıktık
        }

        private void LoadFilms()
        {
            using (OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database2.accdb"))
            {
                string query = "SELECT * FROM filmdizilistesi ORDER BY Kimlik"; //veritabanındaki kimlik sırası ile gelmesi için kullandık.
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection);
                DataTable films = new DataTable();
                adapter.Fill(films);
                DisplayFilms(films);
            }
        }

        private void DisplayFilms(DataTable films)
        {
            panel1.Width = ClientSize.Width;
            panel1.Top = 0;
            panel1.Left = 0;

            button1.Height = panel1.Height;
            button1.Width = button1.Height;

            int panelWidth = ClientSize.Width / 3;
            int panelHeight = ClientSize.Height - panel1.Height;

            for (int i = 0; i < films.Rows.Count; i++)
            {
                DataRow row = films.Rows[i];

                Panel panel = new Panel
                {
                    Width = panelWidth,
                    Height = panelHeight,
                    Left = (i % 3) * panelWidth,
                    Top = panel1.Height + (i / 3) * panelHeight,
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.White,
                };

                string imageName = row["poster"].ToString();
                string dirRoot = RootDirectory();
                string imagePath = Path.Combine(dirRoot, "WinFormsApp1", "filmposter", imageName);

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
                    Text = $"{row["filmMiDiziMi"]}\n" +
                           $"{row["filmMiDiziMi"]} Türü: {row["turu"]}\n" +
                           $"{row["filmMiDiziMi"]} Adı: {row["adi"]}\n" +
                           $"{row["filmMiDiziMi"]} Yılı: {row["yil"]}\n" +
                           $"{row["filmMiDiziMi"]} Yapımcısı: {row["yapimci"]}\n" +
                           $"{row["filmMiDiziMi"]} Puanı: {row["puan"]}\n",
                    AutoSize = true,
                    Location = new Point(75, pictureBox.Bottom + 20),
                };

                
                panel.Controls.Add(label); //label in uzunluğunun botton tarafından bilinmesi için önceden panele ekledik

                Button button = new Button
                {
                    Width = pictureBox.Width,
                    Height = 50,
                    Text = "Listeye Ekle",
                    Location = new Point(75, label.Bottom + 20),
                    Tag = row["Kimlik"],

                };
                button.Click += ButtonClick;
                //button.Click += (s, e) => ButtonClick(row);


                panel.Controls.Add(button);
                this.Controls.Add(panel);

            }

            this.HorizontalScroll.Enabled = false;
            this.VerticalScroll.Enabled = true;
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            Button button = sender as Button;

            int filmID = Convert.ToInt32(button.Tag);
            //int filmID = Convert.ToInt32(row["Kimlik"]);
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
                using (OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database2.accdb"))
                {
                    string query = "INSERT INTO izlemeListesi (KullanıcıID, FilmDiziID) VALUES (@KullanıcıID, @FilmDiziID)";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@KullanıcıID", kullaniciID);
                        command.Parameters.AddWithValue("@FilmDiziID", filmID);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("İzleme listenize eklendi");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /*private void FormFilm_Resize(object sender, EventArgs e)
        {
            panel1.Width = ClientSize.Width;
            panel1.Height = (90 * panel1.Width) / 1510;

            int panelwidth = ClientSize.Width / 3;
            int panelheight = ClientSize.Height - panel1.Height;

            int sayac = 0;

            for(int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i] is Panel)
                {
                    Panel pnl = (Panel)Controls[i];
                    pnl.Width = panelwidth;
                    pnl.Height = panelheight;
                    pnl.Top = panel1.Height + (sayac / 3) * pnl.Height;
                    pnl.Left = (sayac % 3) * pnl.Width;

                    foreach(Control control in pnl.Controls)
                    {
                        if(control is PictureBox) 
                        {
                            PictureBox pictureBox = (PictureBox)control;
                            pictureBox.Width = pnl.Width - 150;
                            pictureBox.Left = 75;
                            pictureBox.Top = 75;
                            pictureBox.Height = pnl.Height - 200;

                        }

                        else if(control is Label)
                        {
                            Label label = (Label)control;

                            if (pnl.Controls.Count > 0 && pnl.Controls[0] is PictureBox pb)
                            {
                                label.Location = new Point(75, pb.Bottom + 20);
                            }
                        }

                        else if(control is Button) 
                        {
                            Button button = (Button)control;

                            if (pnl.Controls.Count > 1 && pnl.Controls[1] is Label lbl)
                            {
                                button.Location = new Point(75, lbl.Bottom + 20);
                                button.Width = pnl.Width - 150;
                            }
                        }
                    }
                    sayac++;
                }
            }

            this.HorizontalScroll.Enabled = false;
            this.VerticalScroll.Enabled = true;
        }*/

        private void FormFilm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)//navbardaki geri dönüş tuşu için
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
