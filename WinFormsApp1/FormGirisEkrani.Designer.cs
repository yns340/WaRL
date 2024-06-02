namespace WinFormsApp1
{
    partial class FormGirisEkrani
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGirisEkrani));
            pictureBox1 = new PictureBox();
            textBox1 = new TextBox();
            label2 = new Label();
            textBox2 = new TextBox();
            label1 = new Label();
            button1 = new Button();
            label3 = new Label();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(14, 14);
            pictureBox1.Margin = new Padding(4, 4, 4, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(378, 511);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            textBox1.Location = new Point(511, 220);
            textBox1.Margin = new Padding(4, 4, 4, 4);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(335, 35);
            textBox1.TabIndex = 1;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            label2.AutoSize = true;
            label2.Location = new Point(511, 287);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(54, 30);
            label2.TabIndex = 4;
            label2.Text = "Şifre";
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            textBox2.Location = new Point(511, 320);
            textBox2.Margin = new Padding(4, 4, 4, 4);
            textBox2.Name = "textBox2";
            textBox2.PasswordChar = '*';
            textBox2.Size = new Size(335, 35);
            textBox2.TabIndex = 2;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            label1.AutoSize = true;
            label1.Location = new Point(511, 186);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(127, 30);
            label1.TabIndex = 3;
            label1.Text = "Kullanıcı Adı";
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top;
            button1.Cursor = Cursors.Hand;
            button1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 162);
            button1.Location = new Point(511, 378);
            button1.Margin = new Padding(4, 4, 4, 4);
            button1.Name = "button1";
            button1.Size = new Size(154, 79);
            button1.TabIndex = 6;
            button1.Text = "Giriş Yap";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            label3.AutoSize = true;
            label3.Font = new Font("Showcard Gothic", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(566, 96);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(224, 47);
            label3.TabIndex = 5;
            label3.Text = "UYGULAMA";
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top;
            button2.Cursor = Cursors.Hand;
            button2.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 162);
            button2.Location = new Point(694, 378);
            button2.Margin = new Padding(4, 4, 4, 4);
            button2.Name = "button2";
            button2.Size = new Size(154, 79);
            button2.TabIndex = 7;
            button2.Text = "Kayıt Ol";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // FormGirisEkrani
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(960, 540);
            Controls.Add(button2);
            Controls.Add(pictureBox1);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(textBox2);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(textBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 4, 4, 4);
            Name = "FormGirisEkrani";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "IMDB giriş";
            FormClosing += FormGirisEkrani_FormClosing;
            Load += FormGirisEkrani_Load;
            Resize += FormGirisEkrani_Resize;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private TextBox textBox1;
        private Label label2;
        private TextBox textBox2;
        private Label label1;
        private Button button1;
        private Label label3;
        private Button button2;
    }
}