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
using System.Threading;
using System.Runtime.InteropServices;

namespace HuckleberryLauncher
{
    public partial class MainForm : Form
    {
        private static String host = "https://huckleberry.runsafe.no/";

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
                client.DownloadStringAsync(new Uri(MainForm.host + "latest.txt"));
            }
        }

        public void setLatestContent(String content)
        {
            Action action = delegate { updates_panel.Rtf = content; };
            updates_panel.Invoke(action);
        }

        public void authenticateUser(String username, String password)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadStringCompleted += (sender, e) =>
                {
                    handleAuthenticationResponse(e.Result);
                };
                client.DownloadStringAsync(new Uri(MainForm.host + "auth.php?username=" + username + "&password=" + password));
            }
        }

        public void handleAuthenticationResponse(String response)
        {
            if (response == "invalid")
            {
                this.Invoke((MethodInvoker)delegate()
                {
                    username.Show();
                    password.Show();
                    auth_loading_label.Hide();
                });
            }
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
            {
                password.Focus();
                e.Handled = true;
            }
        }

        private void password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                String username_value = username.Text.Trim();
                String password_value = password.Text.Trim();

                if (username_value.Length > 0 && password_value.Length > 0)
                {
                    username.Hide();
                    password.Hide();
                    auth_loading_label.Show();

                    new Thread(() => authenticateUser(username_value, password_value)).Start();
                }

                e.Handled = true;
            }
        }
    }
}
