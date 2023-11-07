using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Starter1C
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string labelText = label_number.Text; // получаем текст лейбла
            Clipboard.SetText(labelText); // помещаем текст в буфер обмена
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string labelText = label_mail.Text; // получаем текст лейбла
            Clipboard.SetText(labelText); // помещаем текст в буфер обмена
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
