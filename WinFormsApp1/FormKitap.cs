using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class FormKitap : Form
    {
        private string _username;
        private int _kullanıcıID;

        public FormKitap(string username, int kullanıcıID)
        {
            InitializeComponent();
            this._username = username;
            this._kullanıcıID = kullanıcıID;
        }

        private void FormKitap_Load(object sender, EventArgs e)
        {
            LoadKitap();
        }

        private void LoadKitap()
        {
            using (OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database2.accdb"))
            {
                string query = "SELECT * FROM Kitaplar";
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection);
                DataTable kitaplar = new DataTable();
                adapter.Fill(kitaplar);
                DisplayKitaplar(kitaplar);
            }
        }

        private void DisplayKitaplar(DataTable kitaplar)
        {
            int panelWidth = ClientSize.Width / 3;
            int panelHeight = ClientSize.Height;

            for (int i = 0; i < kitaplar.Rows.Count; i++)
            {
                DataRow row = kitaplar.Rows[i];

                Panel panel = new Panel
                {
                    Width = panelWidth,
                    Height = panelHeight,
                    Left = (i % 3) * panelWidth,
                    Top = (i / 3) * panelHeight,
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.White,
                };

                PictureBox pictureBox = new PictureBox
                {
                    ImageLocation = " ", // Burada gerçek resim yolunu kullanabilirsiniz
                    BackColor = Color.Red,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Width = panelWidth - 200,
                    Left = 75,
                    Top = 75,
                    Height = panelHeight - 200,
                };

                Label label = new Label
                {
                    Text = $"Kitabın Adı: {row["KitapAdı"]}\n" +
                           $"Kitabın Türleri: {row["Türler"]}\n" +
                           $"Kitabın Yazarı: {row["Yazar"]}\n",
                    AutoSize = true,
                    Location = new Point(75, pictureBox.Bottom + 20),
                };

                Button button = new Button
                {
                    Width = pictureBox.Width,
                    Height = 75,
                    Text = "Listeye Ekle",
                    Location = new Point(75, label.Bottom + 20),
                    Tag = row["KitapID"],

                };
                button.Click += ButtonClick;
                //button.Click += (s, e) => ButtonClick(row);

                panel.Controls.Add(pictureBox);
                panel.Controls.Add(label);
                panel.Controls.Add(button);
                this.Controls.Add(panel);

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
                using (OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database2.accdb"))
                {
                    string query = "INSERT INTO okumaListesi (KullanıcıID, KitapID) VALUES (@KullanıcıID, @KitapID)";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@KullanıcıID", kullaniciID);
                        command.Parameters.AddWithValue("@KitapID", KitapID);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Okuma listenize eklendi");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FormKitap_Resize(object sender, EventArgs e)
        {
            int panelWidth = ClientSize.Width / 3;
            int panelHeight = ClientSize.Height;

            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i] is Panel)
                {
                    Panel panel = (Panel)Controls[i];
                    panel.Width = panelWidth;
                    panel.Height = panelHeight;
                    panel.Left = (i % 3) * panelWidth;
                    panel.Top = (i / 3) * panelHeight;

                    foreach (Control control in panel.Controls)
                    {
                        if (control is PictureBox)
                        {
                            PictureBox pictureBox = (PictureBox)control;
                            pictureBox.Width = panelWidth - 150;
                            pictureBox.Height = panelHeight - 450;
                            pictureBox.Left = 75;
                            pictureBox.Top = 75;
                        }
                        else if (control is Label)
                        {
                            Label label = (Label)control;
                            label.Location = new Point(75, panel.Controls[0].Bottom + 20);
                        }
                        else if (control is Button)
                        {
                            Button button = (Button)control;
                            button.Location = new Point(75, panel.Controls[1].Bottom + 20);
                            button.Width = panelWidth - 150;
                        }
                    }
                }
            }
            this.HorizontalScroll.Enabled = false;
            this.VerticalScroll.Enabled = true;
        }

        private void FormKitap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }
    }
}
