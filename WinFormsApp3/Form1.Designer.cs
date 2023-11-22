namespace Starter1C
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            label2 = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            dataGridView1 = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            bindingSource1 = new BindingSource(components);
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            yt_Button1 = new yt_Button();
            yt_Button4 = new yt_Button();
            yt_Button2 = new yt_Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(12, -2);
            label1.Name = "label1";
            label1.Size = new Size(220, 25);
            label1.TabIndex = 1;
            label1.Text = "Запуск 1С Предприятие";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.FromArgb(78, 147, 137);
            label2.Location = new Point(12, 48);
            label2.Name = "label2";
            label2.Size = new Size(179, 20);
            label2.TabIndex = 2;
            label2.Text = "Информационные базы";
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(250, 250, 250);
            button1.FlatAppearance.BorderColor = Color.FromArgb(250, 250, 250);
            button1.FlatStyle = FlatStyle.Popup;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.Location = new Point(680, 71);
            button1.Name = "button1";
            button1.Size = new Size(122, 25);
            button1.TabIndex = 3;
            button1.Text = "1С Предприятие";
            button1.TextAlign = ContentAlignment.MiddleRight;
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(680, 344);
            button2.Name = "button2";
            button2.Size = new Size(122, 25);
            button2.TabIndex = 4;
            button2.Text = "Выход";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.FlatStyle = FlatStyle.System;
            button3.Location = new Point(680, 102);
            button3.Name = "button3";
            button3.Size = new Size(122, 25);
            button3.TabIndex = 6;
            button3.Text = "Тех. поддержка";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.BackgroundColor = SystemColors.ButtonHighlight;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2 });
            dataGridView1.GridColor = Color.White;
            dataGridView1.Location = new Point(12, 71);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(342, 264);
            dataGridView1.TabIndex = 7;
            // 
            // Column1
            // 
            Column1.HeaderText = "Имя";
            Column1.Name = "Column1";
            // 
            // Column2
            // 
            Column2.HeaderText = "Дата удаления";
            Column2.Name = "Column2";
            // 
            // yt_Button1
            // 
            yt_Button1.BackColor = Color.White;
            yt_Button1.BorderColor = Color.Black;
            yt_Button1.BorderWidth = 43;
            yt_Button1.ButtonImage = (Image)resources.GetObject("yt_Button1.ButtonImage");
            yt_Button1.ForeColor = Color.Black;
            yt_Button1.IsVisible = true;
            yt_Button1.Location = new Point(360, 71);
            yt_Button1.Name = "yt_Button1";
            yt_Button1.Size = new Size(122, 25);
            yt_Button1.TabIndex = 8;
            yt_Button1.Text = "1С Предприятие";
            yt_Button1.Click += yt_Button1_Click;
            // 
            // yt_Button4
            // 
            yt_Button4.BackColor = Color.White;
            yt_Button4.BorderColor = Color.Black;
            yt_Button4.BorderWidth = 1;
            yt_Button4.ButtonImage = null;
            yt_Button4.ForeColor = Color.Black;
            yt_Button4.IsVisible = true;
            yt_Button4.Location = new Point(360, 102);
            yt_Button4.Name = "yt_Button4";
            yt_Button4.Size = new Size(122, 25);
            yt_Button4.TabIndex = 11;
            yt_Button4.Text = "Тех. поддержка";
            yt_Button4.Click += yt_Button4_Click;
            // 
            // yt_Button2
            // 
            yt_Button2.BackColor = Color.White;
            yt_Button2.BorderColor = Color.Black;
            yt_Button2.BorderWidth = 1;
            yt_Button2.ButtonImage = null;
            yt_Button2.ForeColor = Color.Black;
            yt_Button2.IsVisible = true;
            yt_Button2.Location = new Point(360, 344);
            yt_Button2.Name = "yt_Button2";
            yt_Button2.Size = new Size(122, 25);
            yt_Button2.TabIndex = 12;
            yt_Button2.Text = "Выход";
            yt_Button2.Click += yt_Button2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(487, 380);
            Controls.Add(yt_Button2);
            Controls.Add(yt_Button4);
            Controls.Add(yt_Button1);
            Controls.Add(dataGridView1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Form1";
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label label2;
        private Button button1;
        private Button button2;
        private Button button3;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private BindingSource bindingSource1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private yt_Button yt_Button1;
        private yt_Button yt_Button4;
        private yt_Button yt_Button2;
    }
}