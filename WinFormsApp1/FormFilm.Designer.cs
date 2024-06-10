namespace WinFormsApp1
{
    partial class FormFilm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFilm));
            panel1 = new Panel();
            checkBox10 = new CheckBox();
            checkBox9 = new CheckBox();
            checkBox8 = new CheckBox();
            checkBox7 = new CheckBox();
            checkBox6 = new CheckBox();
            checkBox5 = new CheckBox();
            checkBox4 = new CheckBox();
            checkBox3 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox1 = new CheckBox();
            button1 = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            panel1.Controls.Add(checkBox10);
            panel1.Controls.Add(checkBox9);
            panel1.Controls.Add(checkBox8);
            panel1.Controls.Add(checkBox7);
            panel1.Controls.Add(checkBox6);
            panel1.Controls.Add(checkBox5);
            panel1.Controls.Add(checkBox4);
            panel1.Controls.Add(checkBox3);
            panel1.Controls.Add(checkBox2);
            panel1.Controls.Add(checkBox1);
            panel1.Controls.Add(button1);
            panel1.Location = new Point(1, 1);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1210, 72);
            panel1.TabIndex = 0;
            // 
            // checkBox10
            // 
            checkBox10.AutoSize = true;
            checkBox10.Location = new Point(444, 23);
            checkBox10.Name = "checkBox10";
            checkBox10.Size = new Size(104, 24);
            checkBox10.TabIndex = 8;
            checkBox10.Text = "Animasyon";
            checkBox10.UseVisualStyleBackColor = true;
            checkBox10.CheckedChanged += CheckBox_CheckedChanged;
            // 
            // checkBox9
            // 
            checkBox9.AutoSize = true;
            checkBox9.Location = new Point(786, 23);
            checkBox9.Name = "checkBox9";
            checkBox9.Size = new Size(68, 24);
            checkBox9.TabIndex = 1;
            checkBox9.Text = "Dram";
            checkBox9.UseVisualStyleBackColor = true;
            checkBox9.CheckedChanged += CheckBox_CheckedChanged;
            // 
            // checkBox8
            // 
            checkBox8.AutoSize = true;
            checkBox8.Location = new Point(640, 23);
            checkBox8.Name = "checkBox8";
            checkBox8.Size = new Size(140, 24);
            checkBox8.TabIndex = 7;
            checkBox8.Text = "Süper Kahraman";
            checkBox8.UseVisualStyleBackColor = true;
            checkBox8.CheckedChanged += CheckBox_CheckedChanged;
            // 
            // checkBox7
            // 
            checkBox7.AutoSize = true;
            checkBox7.Location = new Point(1061, 23);
            checkBox7.Name = "checkBox7";
            checkBox7.Size = new Size(108, 24);
            checkBox7.TabIndex = 1;
            checkBox7.Text = "Bilim Kurgu";
            checkBox7.UseVisualStyleBackColor = true;
            checkBox7.CheckedChanged += CheckBox_CheckedChanged;
            // 
            // checkBox6
            // 
            checkBox6.AutoSize = true;
            checkBox6.Location = new Point(975, 23);
            checkBox6.Name = "checkBox6";
            checkBox6.Size = new Size(80, 24);
            checkBox6.TabIndex = 6;
            checkBox6.Text = "Polisiye";
            checkBox6.UseVisualStyleBackColor = true;
            checkBox6.CheckedChanged += CheckBox_CheckedChanged;
            // 
            // checkBox5
            // 
            checkBox5.AutoSize = true;
            checkBox5.Location = new Point(870, 23);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new Size(82, 24);
            checkBox5.TabIndex = 5;
            checkBox5.Text = "Aksiyon";
            checkBox5.UseVisualStyleBackColor = true;
            checkBox5.CheckedChanged += CheckBox_CheckedChanged;
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Location = new Point(571, 23);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(63, 24);
            checkBox4.TabIndex = 4;
            checkBox4.Text = "Uzay";
            checkBox4.UseVisualStyleBackColor = true;
            checkBox4.CheckedChanged += CheckBox_CheckedChanged;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(362, 23);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(62, 24);
            checkBox3.TabIndex = 3;
            checkBox3.Text = "Tarih";
            checkBox3.UseVisualStyleBackColor = true;
            checkBox3.CheckedChanged += CheckBox_CheckedChanged;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(171, 23);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(77, 24);
            checkBox2.TabIndex = 2;
            checkBox2.Text = "Sitcom";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += CheckBox_CheckedChanged;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(254, 23);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(88, 24);
            checkBox1.TabIndex = 1;
            checkBox1.Text = "Fantastik";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += CheckBox_CheckedChanged;
            // 
            // button1
            // 
            button1.BackgroundImage = (Image)resources.GetObject("button1.BackgroundImage");
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.Location = new Point(0, 0);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(72, 72);
            button1.TabIndex = 0;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // FormFilm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = SystemColors.ControlDark;
            ClientSize = new Size(1212, 694);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2);
            Name = "FormFilm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WaRL";
            FormClosing += FormFilm_FormClosing;
            Load += FormFilm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button button1;
        private CheckBox checkBox6;
        private CheckBox checkBox5;
        private CheckBox checkBox4;
        private CheckBox checkBox3;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private CheckBox checkBox8;
        private CheckBox checkBox7;
        private CheckBox checkBox10;
        private CheckBox checkBox9;
    }
}