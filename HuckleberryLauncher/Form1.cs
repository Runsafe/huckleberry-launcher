using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HuckleberryLauncher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void fieldBlur(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Length == 0)
            {
                textBox.Text = textBox.Tag.ToString();
                textBox.ForeColor = Color.Gray;

                if (textBox.Name == "password")
                    textBox.PasswordChar = '\0';
            }
        }

        private void fieldFocus(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.ForeColor == Color.Gray)
            {
                textBox.ForeColor = Color.Black;
                textBox.Text = String.Empty;

                if (textBox.Name == "password")
                    textBox.PasswordChar = '*';
            }
        }
    }
}
