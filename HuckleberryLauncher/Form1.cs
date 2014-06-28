using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Collections.Specialized;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace HuckleberryLauncher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            player_splash.Image = (Image) Properties.Resources.ResourceManager.GetObject("player_shot_" + new Random().Next(1, 8));
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

        private void exitButton_MouseEnter(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackgroundImage = global::HuckleberryLauncher.Properties.Resources.exit_button_hover;
        }

        private void exitButton_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackgroundImage = global::HuckleberryLauncher.Properties.Resources.exit_button_normal;
        }

        private void exitButton_MouseClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
