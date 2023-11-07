namespace Starter1C
{
    partial class Form2
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
            label1 = new Label();
            label_number = new Label();
            label3 = new Label();
            label_mail = new Label();
            button1 = new Button();
            button2 = new Button();
            label5 = new Label();
            button3 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(67, 118);
            label1.Name = "label1";
            label1.Size = new Size(57, 15);
            label1.TabIndex = 0;
            label1.Text = "Телефон";
            // 
            // label_number
            // 
            label_number.AutoSize = true;
            label_number.Location = new Point(153, 118);
            label_number.Name = "label_number";
            label_number.Size = new Size(85, 15);
            label_number.TabIndex = 1;
            label_number.Text = "8 905 925 55 02";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(67, 169);
            label3.Name = "label3";
            label3.Size = new Size(41, 15);
            label3.TabIndex = 2;
            label3.Text = "Почта";
            // 
            // label_mail
            // 
            label_mail.AutoSize = true;
            label_mail.Location = new Point(153, 169);
            label_mail.Name = "label_mail";
            label_mail.Size = new Size(132, 15);
            label_mail.TabIndex = 3;
            label_mail.Text = "support@sibgroup22.ru";
            // 
            // button1
            // 
            button1.Location = new Point(330, 114);
            button1.Name = "button1";
            button1.Size = new Size(101, 23);
            button1.TabIndex = 4;
            button1.Text = "Скопировать";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(330, 161);
            button2.Name = "button2";
            button2.Size = new Size(101, 23);
            button2.TabIndex = 5;
            button2.Text = "Скопировать";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 19F, FontStyle.Italic, GraphicsUnit.Point);
            label5.Location = new Point(67, 32);
            label5.Name = "label5";
            label5.Size = new Size(299, 36);
            label5.TabIndex = 6;
            label5.Text = "Техническая поддержка";
            // 
            // button3
            // 
            button3.Location = new Point(433, 241);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 7;
            button3.Text = "Закрыть";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(520, 276);
            Controls.Add(button3);
            Controls.Add(label5);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label_mail);
            Controls.Add(label3);
            Controls.Add(label_number);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form2";
            Text = "Тех. поддержка";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label_number;
        private Label label3;
        private Label label_mail;
        private Button button1;
        private Button button2;
        private Label label5;
        private Button button3;
    }
}