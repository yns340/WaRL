using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class FormFilm : Form
    {
        public FormFilm()
        {
            InitializeComponent();
        }

        private void FormFilm_Load(object sender, EventArgs e)
        {
            LoadFilms();
        }

        private void LoadFilms()
        {
            using (OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source= Database2.accdb"))
            {
                string query = "SELECT * FROM filmdizilistesi";
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection);
                DataTable films = new DataTable();
                adapter.Fill(films);
                DisplayFilms(films);
            }
        }

        private void DisplayFilms(DataTable films)
        {
            int panelWidth = ClientSize.Width / 3; // Her panelin genişliği formun genişliğinin üçte biri
            int panelHeight = ClientSize.Height; // Her panelin yüksekliği formun yüksekliği kadar

            for (int i = 0; i < films.Rows.Count; i++)
            {
                DataRow row = films.Rows[i];

                Panel panel = new Panel
                {
                    Width = panelWidth,
                    Height = panelHeight,
                    Left = (i % 3) * panelWidth, // Her panelin sol kenarı, önceki panelin sağ kenarından başlar
                    Top = (i / 3) * panelHeight,
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.White,
                };

                

                PictureBox pictureBox = new PictureBox
                {

                    ImageLocation = " ",
                    BackColor = Color.Red,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Width = panelWidth - 200,
                    Left = 75,
                    Top = 75,
                    Height = panelHeight - 200,
                };

                Label label = new Label
                {
                    Text = $"{row["filmMiDiziMi"]} Adı: {row["adi"]}\n" +
                           $"{row["filmMiDiziMi"]} Yılı: {row["yil"]}\n" +
                           $"{row["filmMiDiziMi"]} Yönetmeni: {row["yonetmen"]}\n" +
                           $"{row["filmMiDiziMi"]} Puanı: {row["puan"]}\n",
                    AutoSize = true,
                    Location = new System.Drawing.Point(75, pictureBox.Bottom + 20),
                };

                Button button = new Button
                {
                    Width = pictureBox.Width,
                    Height = 75,
                    Text = "Listeye Ekle",
                    Location = new System.Drawing.Point(75, label.Bottom + 20),
                };

                button.Click += (s, e) => ButtonClick(row);


                panel.Controls.Add(pictureBox);
                panel.Controls.Add(label);
                panel.Controls.Add(button);
                this.Controls.Add(panel);
            }

            this.HorizontalScroll.Enabled = false;
            this.VerticalScroll.Enabled = true;
        }

        private void ButtonClick(DataRow row)
        {
            MessageBox.Show("İzleme Listenize eklendi");
        }

        private void FormFilm_Resize(object sender, EventArgs e)
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

        private void FormFilm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }
    }
}
