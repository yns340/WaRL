namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.FormClosing += new FormClosingEventHandler(Form1_Closing);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;

            ResizePanels();
            ResizePictureBoxs();
            ResizeButtons();
        }

        private void Form_1Resize(object sender, EventArgs e)
        {
            ResizePanels();
            ResizePictureBoxs();
            ResizeButtons();

         

        }

        private void ResizePanels()
        {
            int panelWidth = this.ClientSize.Width;
            panel1.Width = panelWidth / 2;
            panel2.Width = panelWidth / 2;


            int panelHeight = this.ClientSize.Height;
            panel1.Height = panelHeight;
            panel2.Height = panelHeight;



            panel1.Left = 0;
            panel2.Left = panel1.Width;

        }

        private void ResizePictureBoxs()
        {
            if (pictureBox1 != null)
            {
                pictureBox1.ClientSize = new Size(this.ClientSize.Width / 2, (62 * (this.ClientSize.Width / 2)) / 92);
            }

            if (pictureBox2 != null)
            {
                pictureBox2.ClientSize = new Size(this.ClientSize.Width / 2, (62 * (this.ClientSize.Width / 2)) / 92);
            }

        }

        private void ResizeButtons()
        {
            button1.ClientSize = new Size(this.ClientSize.Width / 2, panel1.Height - pictureBox1.Height);
            button2.ClientSize = new Size(this.ClientSize.Width / 2, panel2.Height - pictureBox1.Height);
            button1.Top = pictureBox1.Bottom;
            button2.Top = pictureBox2.Bottom;

        }



        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ClientSize = new Size(this.ClientSize.Width, this.ClientSize.Height);

            if (this.WindowState == FormWindowState.Maximized)
            {
                form2.WindowState = FormWindowState.Maximized;
            }

            this.Hide();
            form2.Show();
        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ClientSize = new Size(this.ClientSize.Width, this.ClientSize.Height);
            
            if (this.WindowState == FormWindowState.Maximized)
                {
                    form3.WindowState = FormWindowState.Maximized;
                }
            this.Hide();
            form3.Show();
        }
    }
}
