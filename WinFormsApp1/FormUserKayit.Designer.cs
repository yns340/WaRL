namespace WinFormsApp1
{
    partial class FormUserKayit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUserKayit));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 162);
            label1.Location = new Point(220, 320);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(287, 30);
            label1.TabIndex = 0;
            label1.Text = "\"Yeni kullanıcı ekleyebilirsiniz.\"";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 162);
            label2.Location = new Point(142, 350);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(452, 30);
            label2.TabIndex = 1;
            label2.Text = "\"Kullanıcı eklendikten sonra bu ekranı kapatınız.\"";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(142, 9);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(127, 30);
            label3.TabIndex = 2;
            label3.Text = "Kullanıcı Adı";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(142, 97);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(54, 30);
            label4.TabIndex = 3;
            label4.Text = "Şifre";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(142, 43);
            textBox1.Margin = new Padding(4);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(479, 35);
            textBox1.TabIndex = 4;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(142, 131);
            textBox2.Margin = new Padding(4);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(479, 35);
            textBox2.TabIndex = 5;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            button1.Location = new Point(272, 174);
            button1.Margin = new Padding(4);
            button1.Name = "button1";
            button1.Size = new Size(235, 41);
            button1.TabIndex = 6;
            button1.Text = "Kullanıcı Ekle";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            button2.Location = new Point(272, 223);
            button2.Margin = new Padding(4);
            button2.Name = "button2";
            button2.Size = new Size(235, 41);
            button2.TabIndex = 7;
            button2.Text = "Vazgeç";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // FormUserKayit
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(744, 391);
            Controls.Add(button2);
            Controls.Add(label3);
            Controls.Add(button1);
            Controls.Add(textBox2);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            Name = "FormUserKayit";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Kullanıcı Kayıt";
            FormClosing += FormUserKayit_FormClosing;
            Load += FormUserKayit_Load;
            Resize += FormUserKayit_Resize;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBox1;
        private TextBox textBox2;
        private Button button1;
        private Button button2;
    }
}