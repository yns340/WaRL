namespace WinFormsApp1
{
    partial class Form3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            button1 = new Button();
            label1 = new Label();
            button2 = new Button();
            button3 = new Button();
            label2 = new Label();
            label3 = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackgroundImage = (Image)resources.GetObject("button1.BackgroundImage");
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.Cursor = Cursors.Hand;
            button1.Location = new Point(68, 111);
            button1.Name = "button1";
            button1.Size = new Size(117, 117);
            button1.TabIndex = 0;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.BackColor = Color.White;
            label1.Font = new Font("Showcard Gothic", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(759, 123);
            label1.Name = "label1";
            label1.Size = new Size(289, 103);
            label1.TabIndex = 1;
            label1.Text = "KİTAP";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button2
            // 
            button2.BackgroundImage = (Image)resources.GetObject("button2.BackgroundImage");
            button2.BackgroundImageLayout = ImageLayout.Stretch;
            button2.Cursor = Cursors.Hand;
            button2.Location = new Point(339, 404);
            button2.Name = "button2";
            button2.Size = new Size(358, 351);
            button2.TabIndex = 2;
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.BackgroundImage = (Image)resources.GetObject("button3.BackgroundImage");
            button3.BackgroundImageLayout = ImageLayout.Zoom;
            button3.Cursor = Cursors.Hand;
            button3.Location = new Point(1161, 404);
            button3.Name = "button3";
            button3.Size = new Size(358, 351);
            button3.TabIndex = 3;
            button3.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Stencil", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(382, 759);
            label2.Name = "label2";
            label2.Size = new Size(272, 50);
            label2.TabIndex = 4;
            label2.Text = "KÜTÜPHANE";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Stencil", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(1188, 759);
            label3.Name = "label3";
            label3.Size = new Size(322, 50);
            label3.TabIndex = 5;
            label3.Text = "OKUMA LİSTESİ";
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1818, 1042);
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1818, 1041);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form3";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "IMDB";
            FormClosing += Form3_Closing;
            Load += Form3_Load;
            Resize += Form3_Resize;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label1;
        private Button button2;
        private Button button3;
        private Label label2;
        private Label label3;
        private PictureBox pictureBox1;
    }
}