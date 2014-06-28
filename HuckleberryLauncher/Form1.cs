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
using System.Threading;
using System.Runtime.InteropServices;

namespace HuckleberryLauncher
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            player_splash.Image = (Image) Properties.Resources.ResourceManager.GetObject("player_shot_" + new Random().Next(1, 8));
            new Thread(() => queryLatest()).Start();
        }

        public void queryLatest()
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadStringCompleted += (sender, e) =>
                {
                    setLatestContent(e.Result);
                };
                client.DownloadStringAsync(new Uri("https://huckleberry.runsafe.no/latest.txt"));
            }
        }

        public void setLatestContent(String content)
        {
            Action action = delegate { updates_panel.Rtf = content; };
            updates_panel.Invoke(action);
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

        private void updates_panel_Enter(object sender, EventArgs e)
        {
            focus_holder.Focus();
        }

        private void username_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                password.Focus();
        }

        private void password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                // Something.
            }
        }
    }
}
